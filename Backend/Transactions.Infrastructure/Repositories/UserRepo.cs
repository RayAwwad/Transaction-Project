using Microsoft.EntityFrameworkCore;
using TransactionProject.Data;
using Transactions.Application.Interfaces;
using Transactions.Domain.Entities;

namespace Transactions.Infrastructure.Repositories
{
    public class UserRepo : IUserRepo
    {
        private readonly ApplicationDbContext dbContext;

        public UserRepo(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<User> GetEmailAsync(string email)
        {
            return await dbContext.Users.FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task AddUserAsync(User user)
        {
            dbContext.Users.Add(user);
            await dbContext.SaveChangesAsync();
        }

        public async Task<bool> UserExistsAsync(string email)
        {
            return await dbContext.Users.AnyAsync(x => x.Email == email);
        }

    }
}
