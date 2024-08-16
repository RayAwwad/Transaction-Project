using Transactions.Domain.Entities;

namespace Transactions.Application.Interfaces
{
    public interface ITransactionRepo
    {
        Task<User> GetUserByIdAsync(int userId);
        Task CreateTransactionAsync(Transaction transaction);
        Task SaveChangesAsync();
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
    }
}
