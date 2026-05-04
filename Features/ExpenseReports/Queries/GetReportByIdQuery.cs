using ExpenseHandler.Data;
using ExpenseHandler.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace ExpenseHandler.Features.ExpenseReports.Queries
{
    public class GetReportByIdQuery
    {
        private readonly AppDbContext _context;

        public GetReportByIdQuery(AppDbContext context)
        {
            _context = context;
        }

        public ExpenseReport Execute(Guid reportId)
        {
            return _context.ExpenseReports
                .Include(r => r.Employee)       
                .Include(r => r.Items)          
                .ThenInclude(i => i.Category)   
                .FirstOrDefault(r => r.Id == reportId);
        }
    }
}
