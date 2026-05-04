using ExpenseHandler.Entities;
using Microsoft.EntityFrameworkCore;
namespace ExpenseHandler.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ExpenseReport> ExpenseReports { get; set; }
        public DbSet<ExpenseItem> ExpenseItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            SeedData.Seed(modelBuilder);
            base.OnModelCreating(modelBuilder);

        }
    }
}
