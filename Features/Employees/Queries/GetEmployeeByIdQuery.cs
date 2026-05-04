using ExpenseHandler.Data;
using ExpenseHandler.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace ExpenseHandler.Features.Employees.Queries
{
    public class GetEmployeeByIdQuery
    {
        private readonly AppDbContext _context;

        public GetEmployeeByIdQuery(AppDbContext context)
        {
            _context = context;
        }

        public Employee GetById(Guid employeeId)
        {
            return _context.Employees
                .AsNoTracking()
                .Include(e => e.Manager)                        
                .Include(e => e.ExpenseReports)                 
                    .ThenInclude(r => r.Items)                  
                        .ThenInclude(i => i.Category)         
                .FirstOrDefault(e => e.Id == employeeId);
        }
    }
}
