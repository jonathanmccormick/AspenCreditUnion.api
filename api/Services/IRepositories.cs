using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AspenCreditUnion.api.Models;

namespace AspenCreditUnion.api.Services
{
    public interface IAccountRepository
    {
        Task<Account> GetAccountAsync(Guid accountId);
        Task UpdateAccountAsync(Account account);
    }

    public interface ILoanRepository
    {
        Task<Loan> GetLoanAsync(Guid loanId);
        Task UpdateLoanAsync(Loan loan);
    }

    public interface ITransactionRepository
    {
        Task<IEnumerable<Transaction>> GetUserTransactionsAsync(string userId);
        Task<Transaction> CreateTransactionAsync(Transaction transaction);
        Task<int> GetMonthlyTransactionCountAsync(Guid accountId);
    }
}