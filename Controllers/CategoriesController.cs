using ExpenseHandler.Data;
using ExpenseHandler.Features.Categories.Commands;
using ExpenseHandler.Features.Categories.Queries;
using ExpenseHandler.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ExpenseHandler.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly AppDbContext _context;

        public CategoriesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Categories
        public IActionResult Index()
        {
            var query = new ListCategoriesQuery(_context);
            var categories = query.Execute();

            // Map entities -> ViewModels
            var model = categories.Select(c => new CategoryViewModel
            {
                Id = c.Id,
                Name = c.Name
            }).ToList();

            return View(model);
        }

        // GET: Categories/Create
        public IActionResult Create()
        {
            return View(new CategoryViewModel());
        }

        // POST: Categories/Create
        [HttpPost]
        public IActionResult Create(CategoryViewModel model)
        {
            if (!ModelState.IsValid || string.IsNullOrWhiteSpace(model.Name))
            {
                ModelState.AddModelError("", "Category name is required.");
                return View(model);
            }

            var command = new CreateCategoryCommand(_context);
            command.Execute(model.Name);

            return RedirectToAction("Index");
        }
    }
}
