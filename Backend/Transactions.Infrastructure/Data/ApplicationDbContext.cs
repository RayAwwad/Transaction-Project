
using Microsoft.EntityFrameworkCore;
using Transactions.Domain.Entities;




namespace TransactionProject.Data 
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

       public DbSet<User> Users {get; set;}
       public DbSet<Transaction> Transactions {get; set;}

    }
}
