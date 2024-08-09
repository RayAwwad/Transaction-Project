using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using TransactionProject.Data;
using TransactionProject.Models;
using TransactionProject.Models.Entities;

namespace TransactionProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TransactionController : ControllerBase
    {
        private readonly ApplicationDbContext DbContext;

        public TransactionController(ApplicationDbContext DbContext)
        {
            this.DbContext = DbContext;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTransaction(TransactionDto transactionDto)
        {
            //lets me get the logged in user id
            var senderId = GetUserIdFromToken();


            var sender = await DbContext.Users.FindAsync(senderId); 
            var receiver = await DbContext.Users.FindAsync(transactionDto.ReceiverId);

            if (sender == null)
                return NotFound("Sender not found");

            if (receiver == null)
                return NotFound("Receiver not found");

            if (sender.Balance < transactionDto.Amount)
                return BadRequest("Insufficient balance");

            using var transaction = await DbContext.Database.BeginTransactionAsync();

            sender.Balance -= transactionDto.Amount;
            receiver.Balance += transactionDto.Amount;

            var transactionRecord = new Transactions
            {
                SenderId = senderId.Value,
                ReceiverId = transactionDto.ReceiverId,
                Amount = transactionDto.Amount
            };
           

            DbContext.Transactions.Add(transactionRecord);
            await DbContext.SaveChangesAsync();
            await transaction.CommitAsync();

            return Ok(transactionRecord);
        }

        private int? GetUserIdFromToken()
        {
            //contains all the info of the user such as his id
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                //i get the user id from the whole identity
                var userIdClaim = identity.FindFirst("Id");
                if (userIdClaim != null)
                {
                    return int.Parse(userIdClaim.Value);
                }
            }
            return null;
        }
    }
}
