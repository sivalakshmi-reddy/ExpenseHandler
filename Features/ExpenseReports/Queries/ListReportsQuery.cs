using ExpenseHandler.Data;
using ExpenseHandler.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace ExpenseHandler.Features.ExpenseReports.Queries
{
    public class ListReportsQuery
    {
        private readonly AppDbContext _context;

        public ListReportsQuery(AppDbContext context)
        {
            _context = context;
        }

        public List<ExpenseReport> Execute()
        {
            // Ensure it always returns a list (never null)
            return _context.ExpenseReports
                .Include(r => r.Employee)
                .Include(r => r.Items)
                    .ThenInclude(i => i.Category)
                .ToList();
        }
    }
}
