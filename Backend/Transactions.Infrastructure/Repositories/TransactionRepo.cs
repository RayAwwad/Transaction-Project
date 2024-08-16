using Microsoft.EntityFrameworkCore;
using TransactionProject.Data;
using Transactions.Application.Interfaces;
using Transactions.Domain.Entities;

namespace Transactions.Infrastructure.Repositories
{
    public class TransactionRepo: ITransactionRepo
    {
        private readonly ApplicationDbContext dbContext;

        public TransactionRepo(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<User> GetUserByIdAsync(int userId)
        {
            return await dbContext.Users.FindAsync(userId);
        }

        public async Task CreateTransactionAsync(Transaction transaction)
        {
            dbContext.Transactions.Add(transaction);
            await dbContext.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await dbContext.SaveChangesAsync();
        }

        public async Task BeginTransactionAsync()
        {
            await dbContext.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            await dbContext.Database.CommitTransactionAsync();
        }
    }
}
