using ExpenseHandler.Data;
using ExpenseHandler.Entities;

namespace ExpenseHandler.Features.Categories.Commands
{
    public class CreateCategoryCommand
    {
        private readonly AppDbContext _context;

        public CreateCategoryCommand(AppDbContext context)
        {
            _context = context;
        }

        public void Execute(string name)
        {
            var category = new Category
            {
                Id = Guid.NewGuid(),
                Name = name
            };

            _context.Categories.Add(category);
            _context.SaveChanges();
        }
    }
}
