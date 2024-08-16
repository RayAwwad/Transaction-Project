using Microsoft.EntityFrameworkCore;
using TransactionProject.Data;
using Transactions.Application.Interfaces;
using Transactions.Domain.Entities;

namespace Transactions.Infrastructure.Repositories
{
    public class UserInfoRepo : IUserInfoRepo
    {
        private readonly ApplicationDbContext dbContext;

        public UserInfoRepo(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<User> GetUserByIdAsync(int id)
        {
            return await dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdateUserAsync(User user)
        {
            dbContext.Users.Update(user);
            await dbContext.SaveChangesAsync();
        }
    }
}
