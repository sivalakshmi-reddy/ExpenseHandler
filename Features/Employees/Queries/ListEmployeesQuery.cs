using ExpenseHandler.Data;
using ExpenseHandler.Entities;
using System.Collections.Generic;
using System.Linq;

namespace ExpenseHandler.Features.Employees.Queries
{
    public class ListEmployeesQuery
    {
        private readonly AppDbContext _context;

        public ListEmployeesQuery(AppDbContext context)
        {
            _context = context;
        }

        public List<Employee> Getall()
        {
            return _context.Employees.ToList();
        }
    }
}
