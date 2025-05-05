using System;
using System.Threading.Tasks;
using AuthService.Models;

namespace AuthService.Services
{
    public interface IAccountRepository
    {
        Task<Account> GetAccountAsync(int accountId);
        Task UpdateAccountAsync(Account account);
    }

    public interface ILoanRepository
    {
        Task<Loan> GetLoanAsync(int loanId);
        Task UpdateLoanAsync(Loan loan);
    }
}