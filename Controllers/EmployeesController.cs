using ExpenseHandler.Data;
using ExpenseHandler.Features.Employees.Commands;
using ExpenseHandler.Features.Employees.Queries;
using ExpenseHandler.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ExpenseHandler.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly AppDbContext _context;

        public EmployeesController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var query = new ListEmployeesQuery(_context);
            var employees = query.Getall();

            var model = employees.Select(e => new EmployeeViewModel
            {
                Id = e.Id,
                Name = e.Name,
                Email = e.Email,
                ManagerName = e.Manager != null ? e.Manager.Name : "-"
            }).ToList();

            return View(model);
        }
        public IActionResult Create()
        {
            ViewBag.Employees = _context.Employees.ToList();
            return View(new EmployeeViewModel());
        }

        [HttpPost]
        public IActionResult Create(EmployeeViewModel model)
        {
            if (ModelState.IsValid)
            {
                var command = new CreateEmployeeCommand(_context);
                command.Create(model.Name, model.Email, model.ManagerId);

                return RedirectToAction(nameof(Index));
            }

            // repopulate dropdown if validation fails
            ViewBag.Employees = _context.Employees.ToList();
            return View(model);
        }

        public IActionResult Details(Guid id)
        {
            var query = new GetEmployeeByIdQuery(_context);
            var employee = query.GetById(id);

            if (employee == null) return NotFound();

            var model = new EmployeeViewModel
            {
                Id = employee.Id,
                Name = employee.Name,
                Email = employee.Email,
                ManagerName = employee.Manager?.Name ?? "-"
            };

            return View(model);
        }
    }
}
