using ExpenseHandler.Data;
using ExpenseHandler.Entities;
using ExpenseHandler.Enums;
using System;

namespace ExpenseHandler.Features.ExpenseReports.Commands
{
    public class CreateReportCommand
    {
        private readonly AppDbContext _context;

        public CreateReportCommand(AppDbContext context)
        {
            _context = context;
        }

        public Guid Execute(Guid employeeId)
        {
            var report = new ExpenseReport
            {
                Id = Guid.NewGuid(),
                EmployeeId = employeeId,
                Status = ReportStatus.Draft,   
                CreatedAt = DateTime.UtcNow
            };

            _context.ExpenseReports.Add(report);
            _context.SaveChanges();

            return report.Id;
        }
    }
}
