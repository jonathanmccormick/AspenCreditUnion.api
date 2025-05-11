using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspenCreditUnion.api.Data;
using AspenCreditUnion.api.Models;
using Microsoft.EntityFrameworkCore;

namespace AspenCreditUnion.api.Services
{
    public interface ITransactionService
    {
        Task<Transaction> TransferBetweenAccounts(string userId, Guid fromAcct, Guid toAcct, decimal amount);
        Task<Transaction> PayLoan(string userId, Guid fromAcct, Guid loanId, decimal amount);
        Task<Transaction> LoanAdvance(string userId, Guid loanId, Guid toAcct, decimal amount);
        Task<IEnumerable<Transaction>> GetUserTransactions(string userId);
    }

    public class TransactionService : ITransactionService
    {
        private readonly IAccountRepository _accountRepo;
        private readonly ILoanRepository _loanRepo;
        private readonly ITransactionRepository _transactionRepo;

        public TransactionService(
            IAccountRepository accountRepo, 
            ILoanRepository loanRepo, 
            ITransactionRepository transactionRepo)
        {
            _accountRepo = accountRepo;
            _loanRepo = loanRepo;
            _transactionRepo = transactionRepo;
        }

        public async Task<Transaction> TransferBetweenAccounts(string userId, Guid fromAcct, Guid toAcct, decimal amount)
        {
            // Validate accounts and balances
            var sourceAccount = await _accountRepo.GetAccountAsync(fromAcct);
            if (sourceAccount == null)
                throw new ArgumentException("Source account not found");
            
            if (sourceAccount.OwnerId != userId)
                throw new InvalidOperationException("You are not authorized to transfer from this account");
                
            var destinationAccount = await _accountRepo.GetAccountAsync(toAcct);
            if (destinationAccount == null)
                throw new ArgumentException("Destination account not found");
            
            if (destinationAccount.OwnerId != userId)
                throw new InvalidOperationException("You are not authorized to transfer to this account");
                
            if (sourceAccount.Balance < amount)
                throw new InvalidOperationException("Insufficient funds");
               
            // Check for transaction limits on money market accounts
            if (sourceAccount is MoneyMarketAccount mmAccount)
            {
                var transactionsThisMonth = await _transactionRepo.GetMonthlyTransactionCountAsync(fromAcct);
                if (transactionsThisMonth >= mmAccount.TransactionsPerMonth)
                    throw new InvalidOperationException("Monthly transaction limit exceeded for this money market account");
            }
                
            // Update balances
            sourceAccount.Balance -= amount;
            destinationAccount.Balance += amount;
            
            await _accountRepo.UpdateAccountAsync(sourceAccount);
            await _accountRepo.UpdateAccountAsync(destinationAccount);
            
            // Save transaction
            var transaction = new Transaction
            {
                Id = Guid.NewGuid(),
                Type = TransactionType.Transfer,
                SourceAccountId = fromAcct,
                DestinationAccountId = toAcct,
                Amount = amount,
                CreatedAt = DateTime.UtcNow,
                UserId = userId
            };
            
            return await _transactionRepo.CreateTransactionAsync(transaction);
        }

        public async Task<Transaction> PayLoan(string userId, Guid fromAcct, Guid loanId, decimal amount)
        {
            // Validate account and loan
            var sourceAccount = await _accountRepo.GetAccountAsync(fromAcct);
            if (sourceAccount == null)
                throw new ArgumentException("Source account not found");
            
            if (sourceAccount.OwnerId != userId)
                throw new InvalidOperationException("You are not authorized to use this account");
                
            var loan = await _loanRepo.GetLoanAsync(loanId);
            if (loan == null)
                throw new ArgumentException("Loan not found");
            
            if (loan.BorrowerId != userId)
                throw new InvalidOperationException("You are not authorized to pay this loan");
                
            if (sourceAccount.Balance < amount)
                throw new InvalidOperationException("Insufficient funds");
            
            if (loan.Principal <= 0)
                throw new InvalidOperationException("This loan has already been paid in full");
                
            // Ensure payment doesn't exceed principal
            decimal adjustedAmount = (loan.Principal < amount) ? loan.Principal : amount;
                
            // Update balance and loan principal
            sourceAccount.Balance -= adjustedAmount;
            loan.Principal -= adjustedAmount;
            
            // If loan is paid off, update status
            if (loan.Principal <= 0)
            {
                loan.Status = LoanStatus.Closed;
                loan.EndDate = DateTime.UtcNow;
            }
            
            await _accountRepo.UpdateAccountAsync(sourceAccount);
            await _loanRepo.UpdateLoanAsync(loan);
            
            // Save transaction
            var transaction = new Transaction
            {
                Id = Guid.NewGuid(),
                Type = TransactionType.LoanPayment,
                SourceAccountId = fromAcct,
                DestinationAccountId = loanId,
                Amount = adjustedAmount,
                CreatedAt = DateTime.UtcNow,
                UserId = userId
            };
            
            return await _transactionRepo.CreateTransactionAsync(transaction);
        }

        public async Task<Transaction> LoanAdvance(string userId, Guid loanId, Guid toAcct, decimal amount)
        {
            // Validate loan and account
            var loan = await _loanRepo.GetLoanAsync(loanId);
            if (loan == null)
                throw new ArgumentException("Loan not found");
            
            if (loan.BorrowerId != userId)
                throw new InvalidOperationException("You are not authorized to advance from this loan");
                
            var destinationAccount = await _accountRepo.GetAccountAsync(toAcct);
            if (destinationAccount == null)
                throw new ArgumentException("Destination account not found");
            
            if (destinationAccount.OwnerId != userId)
                throw new InvalidOperationException("You are not authorized to advance to this account");
            
            // Check if this is a line of credit type loan that can be advanced
            if (!(loan is HelocLoan || loan is PersonalLineOfCreditLoan || loan is CreditCardLoan))
                throw new InvalidOperationException("This loan type does not support advances");
                
            // Check loan status
            if (loan.Status != LoanStatus.Active)
                throw new InvalidOperationException("Advances can only be made from active loans");
                
            var creditLimit = 0m;
            if (loan is HelocLoan heloc)
                creditLimit = heloc.CreditLimit;
            else if (loan is PersonalLineOfCreditLoan ploc)
                creditLimit = ploc.CreditLimit;
            else if (loan is CreditCardLoan cc)
                creditLimit = cc.CreditLimit;
                
            if (loan.Principal + amount > creditLimit)
                throw new InvalidOperationException("This advance would exceed the credit limit");
            
            // Update loan principal and account balance
            loan.Principal += amount;
            destinationAccount.Balance += amount;
            
            await _loanRepo.UpdateLoanAsync(loan);
            await _accountRepo.UpdateAccountAsync(destinationAccount);
            
            // Save transaction
            var transaction = new Transaction
            {
                Id = Guid.NewGuid(),
                Type = TransactionType.LoanAdvance,
                SourceAccountId = loanId,
                DestinationAccountId = toAcct,
                Amount = amount,
                CreatedAt = DateTime.UtcNow,
                UserId = userId
            };
            
            return await _transactionRepo.CreateTransactionAsync(transaction);
        }

        public async Task<IEnumerable<Transaction>> GetUserTransactions(string userId)
        {
            return await _transactionRepo.GetUserTransactionsAsync(userId);
        }
    }
}