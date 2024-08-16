using Transactions.Domain.Entities;

namespace Transactions.Application.Interfaces
{
    public interface IUserInfoRepo
    {
        Task<User> GetUserByIdAsync(int id);
        Task UpdateUserAsync(User user);

    }
}
