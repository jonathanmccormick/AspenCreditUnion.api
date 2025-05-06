using System;
using System.Threading.Tasks;
using AuthService.Models;

namespace AuthService.Services
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
}