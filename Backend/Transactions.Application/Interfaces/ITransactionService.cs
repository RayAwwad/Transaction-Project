using Microsoft.AspNetCore.Mvc;
using TransactionProject.Models;

namespace Transactions.Application.Interfaces
{
    public interface ITransactionService
    {
        Task<IActionResult> CreateTransactionAsync(TransactionDto transactionDto, int senderId);
    }

}
