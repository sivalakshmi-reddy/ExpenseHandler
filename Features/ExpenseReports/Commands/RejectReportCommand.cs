using ExpenseHandler.Data;
using ExpenseHandler.Enums;
using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Linq;

namespace ExpenseHandler.Features.ExpenseReports.Commands
{
    public class RejectReportCommand
    {
        private readonly AppDbContext _context;

        public RejectReportCommand(AppDbContext context)
        {
            _context = context;
        }

        public void Execute(Guid reportId, string managerComment)
        {
            var report = _context.ExpenseReports.FirstOrDefault(r => r.Id == reportId);

            if (report == null)
                throw new Exception("Report not found.");

            if (report.Status != ReportStatus.Submitted)
                throw new Exception("Only submitted reports can be rejected.");

            if (string.IsNullOrWhiteSpace(managerComment))
                throw new Exception("Manager comment is required for rejection.");

            report.Status = ReportStatus.Rejected;
            report.ManagerComment = managerComment;

            _context.SaveChanges();
        }
    }
}
