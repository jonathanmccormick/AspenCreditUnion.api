using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AuthService.Models;
using AuthService.Services;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace AuthService.Controllers
{
    [ApiController]
    [Route("api/transactions")]
    [Authorize]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionsController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTransaction([FromBody] TransactionRequest request)
        {
            if (request == null)
                return BadRequest("Request body is required");
                
            if (request.Amount <= 0)
                return BadRequest("Amount must be positive");
                
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            
            try
            {
                Transaction result;
                switch (request.Type)
                {
                    case TransactionType.Transfer:
                        result = await _transactionService.TransferBetweenAccounts(userId, request.SourceAccountId, request.DestinationAccountId, request.Amount);
                        break;
                    case TransactionType.LoanPayment:
                        result = await _transactionService.PayLoan(userId, request.SourceAccountId, request.DestinationAccountId, request.Amount);
                        break;
                    case TransactionType.LoanAdvance:
                        result = await _transactionService.LoanAdvance(userId, request.SourceAccountId, request.DestinationAccountId, request.Amount);
                        break;
                    default:
                        return BadRequest("Invalid transaction type.");
                }

                return Ok(result);
            }
            catch (System.ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (System.InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}