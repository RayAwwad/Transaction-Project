using Transactions.Domain.Entities;

namespace Transactions.Application.Interfaces
{
    public interface IUserRepo
    {
        Task<User> GetEmailAsync(string email);
        Task AddUserAsync(User user);
        Task<bool> UserExistsAsync(string email);


    }
}
