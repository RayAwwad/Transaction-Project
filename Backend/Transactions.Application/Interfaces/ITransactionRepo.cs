using Transactions.Domain.Entities;

namespace Transactions.Application.Interfaces
{
    public interface ITransactionRepo
    {
        Task<User> GetUserByIdAsync(int userId);
        Task AddTransactionAsync(Transaction transaction);
       
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
    }
}
