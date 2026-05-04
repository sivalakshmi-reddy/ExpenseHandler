using ExpenseHandler.Entities;
using ExpenseHandler.Enums;
using Microsoft.EntityFrameworkCore;

namespace ExpenseHandler.Data
{
    public static class SeedData
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            var managerId = Guid.NewGuid();
            var employee1Id = Guid.NewGuid();
            var employee2Id = Guid.NewGuid();

            modelBuilder.Entity<Employee>().HasData(
                new Employee
                {
                    Id = managerId,
                    Name = "Tharun Manager",
                    Email = "Tharun@company.com",
                    ManagerId = null
                },
                new Employee
                {
                    Id = employee1Id,
                    Name = "Naveen Employee",
                    Email = "Naveen@company.com",
                    ManagerId = managerId
                },
                new Employee
                {
                    Id = employee2Id,
                    Name = "Pavan Employee",
                    Email = "Pavan@company.com",
                    ManagerId = managerId
                }
            );

            // --- Categories ---
            var cat1 = Guid.NewGuid();
            var cat2 = Guid.NewGuid();
            var cat3 = Guid.NewGuid();

            modelBuilder.Entity<Category>().HasData(
                new Category { Id = cat1, Name = "Travel" },
                new Category { Id = cat2, Name = "Meals" },
                new Category { Id = cat3, Name = "Office" }
            );

            // --- One Draft ExpenseReport ---
            var reportId = Guid.NewGuid();

            modelBuilder.Entity<ExpenseReport>().HasData(
                new ExpenseReport
                {
                    Id = reportId,
                    EmployeeId = employee1Id,
                    Status = ReportStatus.Draft,
                    CreatedAt = DateTime.UtcNow,
                    ManagerComment = null 
                }
            );

            // --- One ExpenseItem ---
            modelBuilder.Entity<ExpenseItem>().HasData(
                new ExpenseItem
                {
                    Id = Guid.NewGuid(),
                    ReportId = reportId,
                    CategoryId = cat1,
                    SpendDate = DateTime.UtcNow.Date,
                    Amount = 120,
                    Description = "Taxi to airport"
                }
            );
        }
    }
}
