using Microsoft.EntityFrameworkCore;
using E_G_FinalProject.Models.Entities;
using System.Transactions;

namespace E_G_FinalProject.Services
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<TransactionModel> Transactions { get; set; }
    }
}
