using ExpenseHandler.Data;
using ExpenseHandler.Entities;
using ExpenseHandler.Enums;
using System;

namespace ExpenseHandler.Features.ExpenseReports.Commands
{
    public class AddItemCommand
    {
        private readonly AppDbContext _context;

        public AddItemCommand(AppDbContext context)
        {
            _context = context;
        }

        public void Execute(Guid reportId, Guid categoryId, DateTime spendDate, decimal amount, string description)
        {
            var report = _context.ExpenseReports.Find(reportId);

            if (report == null)
                throw new Exception("Report not found.");

            if (report.Status != ReportStatus.Draft)
                throw new Exception("Cannot add items to a non-draft report.");

            if (amount <= 0)
                throw new Exception("Amount must be greater than 0.");

            if (spendDate > DateTime.Today)
                throw new Exception("Spend date cannot be in the future.");

            var item = new ExpenseItem
            {
                Id = Guid.NewGuid(),
                ReportId = reportId,
                CategoryId = categoryId,
                SpendDate = spendDate,
                Amount = amount,
                Description = description
            };

            _context.ExpenseItems.Add(item);
            _context.SaveChanges();
        }
    }
}
