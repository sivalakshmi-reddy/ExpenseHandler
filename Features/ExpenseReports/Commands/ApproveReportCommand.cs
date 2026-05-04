using ExpenseHandler.Data;
using ExpenseHandler.Enums;
using System;
using System.Linq;

namespace ExpenseHandler.Features.ExpenseReports.Commands
{
    public class ApproveReportCommand
    {
        private readonly AppDbContext _context;

        public ApproveReportCommand(AppDbContext context)
        {
            _context = context;
        }

        public void Execute(Guid reportId)
        {
            var report = _context.ExpenseReports.FirstOrDefault(r => r.Id == reportId);

            if (report == null)
                throw new Exception("Report not found.");

            if (report.Status != ReportStatus.Submitted)
                throw new Exception("Only submitted reports can be approved.");

            report.Status = ReportStatus.Approved;

            _context.SaveChanges();
        }
    }
}
