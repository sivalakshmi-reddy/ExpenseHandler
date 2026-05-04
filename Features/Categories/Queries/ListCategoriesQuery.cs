using ExpenseHandler.Data;
using ExpenseHandler.Entities;


namespace ExpenseHandler.Features.Categories.Queries
{
    public class ListCategoriesQuery
    {
        private readonly AppDbContext _context;

        public ListCategoriesQuery(AppDbContext context)
        {
            _context = context;
        }

        public List<Category> Execute()
        {
            return _context.Categories.ToList();
        }
    }
}
