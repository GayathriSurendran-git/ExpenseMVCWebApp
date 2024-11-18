using Microsoft.EntityFrameworkCore;

namespace ExpenseMVCWebApp.Models
{
    public class ExpensesDbContext : DbContext
    {
        public ExpensesDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Expense> Expenses { get; set; }
    }
}
