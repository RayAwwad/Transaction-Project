using Microsoft.AspNetCore.Mvc;
using TransactionProject.Models;
using Transactions.Application.Interfaces;
using Transactions.Domain.Entities;

namespace Transactions.Application.Services
{
    public class TransactionService: ITransactionService
    {
        private readonly ITransactionRepo transactionRepo;

        public TransactionService(ITransactionRepo transactionRepo)
        {
            this.transactionRepo=transactionRepo;
        }
        public async Task<IActionResult> CreateTransactionAsync(TransactionDto transactionDto, int senderId)
        {
            var sender = await transactionRepo.GetUserByIdAsync(senderId);
            var receiver = await transactionRepo.GetUserByIdAsync(transactionDto.ReceiverId);

            if (sender == null)
                return new NotFoundObjectResult("Sender not found");

            if (receiver == null)
                return new NotFoundObjectResult("Receiver not found");

            if (sender.Balance < transactionDto.Amount)
                return new BadRequestObjectResult("Insufficient balance");

            await transactionRepo.BeginTransactionAsync();

            sender.Balance -= transactionDto.Amount;
            receiver.Balance += transactionDto.Amount;

            var transactionRecord = new Transaction
            {
                SenderId = senderId,
                ReceiverId = transactionDto.ReceiverId,
                Amount = transactionDto.Amount
            };

            await transactionRepo.CreateTransactionAsync(transactionRecord);
            await transactionRepo.SaveChangesAsync();
            await transactionRepo.CommitTransactionAsync();

            return new OkObjectResult(transactionRecord);
        }
    }
}
