using ExpenseHandler.Data;
using ExpenseHandler.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace ExpenseHandler.Features.ExpenseReports.Commands
{
    public class SubmitReportCommand
    {
        private readonly AppDbContext _context;

        public SubmitReportCommand(AppDbContext context)
        {
            _context = context;
        }

        public void Execute(Guid reportId)
        {
            var report = _context.ExpenseReports
                .Include(r => r.Items)
                .FirstOrDefault(r => r.Id == reportId);

            if (report == null)
                throw new Exception("Report not found.");

            if (report.Status != ReportStatus.Draft)
                throw new Exception("Only draft reports can be submitted.");

            if (!report.Items.Any())
                throw new Exception("A report must contain at least one item before submitting.");

            report.Status = ReportStatus.Submitted;
            report.SubmittedAt = DateTime.UtcNow;

            _context.SaveChanges();
        }
    }
}
