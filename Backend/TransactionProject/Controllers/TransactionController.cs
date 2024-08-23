using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using TransactionProject.Data;
using TransactionProject.Models;
using Transactions.Domain.Entities;
using Transactions.Application.Interfaces;
using Transactions.Application.Services;


namespace TransactionProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            this.transactionService = transactionService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTransaction(TransactionDto transactionDto)
        {
            var senderId = GetUserIdFromToken();
            if (senderId == null)
                return Unauthorized();

            return await transactionService.CreateTransactionAsync(transactionDto, senderId.Value);
        }

        private int? GetUserIdFromToken()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var userIdClaim = identity.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim != null)
                {
                    return int.Parse(userIdClaim.Value);
                }
            }
            return null;
        }
    }
}