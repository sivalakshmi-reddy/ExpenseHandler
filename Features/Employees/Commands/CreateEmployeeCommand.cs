using ExpenseHandler.Data;
using ExpenseHandler.Entities;

namespace ExpenseHandler.Features.Employees.Commands
{
    public class CreateEmployeeCommand
    {
        private readonly AppDbContext _context;

        public CreateEmployeeCommand(AppDbContext context)
        {
            _context = context;
        }
        public void Create(string name, string email, Guid? managerId)
        {
            var employee = new Employee
            {
                Id = Guid.NewGuid(),
                Name = name,
                Email = email,
                ManagerId = managerId
            };

            _context.Employees.Add(employee);
            _context.SaveChanges();
        }

    }
}
