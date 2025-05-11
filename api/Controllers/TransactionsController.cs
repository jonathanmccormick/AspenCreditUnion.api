using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using System.Linq;
using AspenCreditUnion.api.Models;
using AspenCreditUnion.api.Services;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.Extensions.Logging;

namespace AspenCreditUnion.api.Controllers
{
    [ApiController]
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/transactions")]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        private readonly ILogger<TransactionsController> _logger;

        public TransactionsController(
            ITransactionService transactionService,
            ILogger<TransactionsController> logger)
        {
            _transactionService = transactionService;
            _logger = logger;
        }

        [HttpPost]
        [Route("")]  // Explicitly specify empty route for POST
        public async Task<IActionResult> CreateTransaction([FromBody] TransactionRequest request)
        {
            if (request == null)
                return BadRequest("Request body is required");
                
            if (request.Amount <= 0)
                return BadRequest("Amount must be positive");
                
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized(new { message = "Invalid token" });
            }
            
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

                return Ok(new
                {
                    transactionId = result.Id,
                    type = result.Type.ToString(),
                    amount = result.Amount,
                    sourceId = result.SourceAccountId,
                    destinationId = result.DestinationAccountId,
                    date = result.CreatedAt
                });
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Invalid transaction request");
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Transaction business rule violation");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error processing transaction");
                return StatusCode(500, "An unexpected error occurred while processing the transaction");
            }
        }

        [HttpGet]
        [Route("history")]  // Change to a different route path
        public async Task<IActionResult> GetTransactions()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized(new { message = "Invalid token" });
            }

            try
            {
                var transactions = await _transactionService.GetUserTransactions(userId);
                
                var result = transactions.Select(t => new
                {
                    id = t.Id,
                    type = t.Type.ToString(),
                    amount = t.Amount,
                    sourceId = t.SourceAccountId,
                    destinationId = t.DestinationAccountId,
                    date = t.CreatedAt
                });
                
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving user transactions");
                return StatusCode(500, "An error occurred while retrieving transactions");
            }
        }
    }
}