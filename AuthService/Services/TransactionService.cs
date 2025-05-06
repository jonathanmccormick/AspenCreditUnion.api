using System;
using System.Threading.Tasks;
using AuthService.Data;
using AuthService.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Services
{
    public interface ITransactionService
    {
        Task<Transaction> TransferBetweenAccounts(string userId, Guid fromAcct, Guid toAcct, decimal amount);
        Task<Transaction> PayLoan(string userId, Guid fromAcct, Guid loanId, decimal amount);
        Task<Transaction> LoanAdvance(string userId, Guid loanId, Guid toAcct, decimal amount);
    }

    public class TransactionService : ITransactionService
    {
        private readonly IAccountRepository _accountRepo;
        private readonly ILoanRepository _loanRepo;
        private readonly ApplicationDbContext _context;

        public TransactionService(IAccountRepository accountRepo, ILoanRepository loanRepo, ApplicationDbContext context)
        {
            _accountRepo = accountRepo;
            _loanRepo = loanRepo;
            _context = context;
        }

        public async Task<Transaction> TransferBetweenAccounts(string userId, Guid fromAcct, Guid toAcct, decimal amount)
        {
            // Validate accounts and balances
            var sourceAccount = await _accountRepo.GetAccountAsync(fromAcct);
            if (sourceAccount == null)
                throw new ArgumentException("Source account not found");
                
            var destinationAccount = await _accountRepo.GetAccountAsync(toAcct);
            if (destinationAccount == null)
                throw new ArgumentException("Destination account not found");
                
            if (sourceAccount.Balance < amount)
                throw new InvalidOperationException("Insufficient funds");
                
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
            
            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();
            
            return transaction;
        }

        public async Task<Transaction> PayLoan(string userId, Guid fromAcct, Guid loanId, decimal amount)
        {
            // Validate account and loan
            var sourceAccount = await _accountRepo.GetAccountAsync(fromAcct);
            if (sourceAccount == null)
                throw new ArgumentException("Source account not found");
                
            var loan = await _loanRepo.GetLoanAsync(loanId);
            if (loan == null)
                throw new ArgumentException("Loan not found");
                
            if (sourceAccount.Balance < amount)
                throw new InvalidOperationException("Insufficient funds");
                
            // Update balance and loan principal
            sourceAccount.Balance -= amount;
            loan.Principal -= amount;
            
            await _accountRepo.UpdateAccountAsync(sourceAccount);
            await _loanRepo.UpdateLoanAsync(loan);
            
            // Save transaction
            var transaction = new Transaction
            {
                Id = Guid.NewGuid(),
                Type = TransactionType.LoanPayment,
                SourceAccountId = fromAcct,
                DestinationAccountId = loanId,
                Amount = amount,
                CreatedAt = DateTime.UtcNow,
                UserId = userId
            };
            
            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();
            
            return transaction;
        }

        public async Task<Transaction> LoanAdvance(string userId, Guid loanId, Guid toAcct, decimal amount)
        {
            // Validate loan and account
            var loan = await _loanRepo.GetLoanAsync(loanId);
            if (loan == null)
                throw new ArgumentException("Loan not found");
                
            var destinationAccount = await _accountRepo.GetAccountAsync(toAcct);
            if (destinationAccount == null)
                throw new ArgumentException("Destination account not found");
            
            // Check if this is a line of credit type loan that can be advanced
            if (!(loan is HelocLoan || loan is PersonalLineOfCreditLoan || loan is CreditCardLoan))
                throw new InvalidOperationException("This loan type does not support advances");
                
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
            
            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();
            
            return transaction;
        }
    }
}