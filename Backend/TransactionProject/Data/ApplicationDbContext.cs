using TransactionProject.Models.Entities;
using Microsoft.EntityFrameworkCore;




namespace TransactionProject.Data 
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

       public DbSet<User> Users {get; set;}
       public DbSet<Transactions> Transactions {get; set;}

    }
}
