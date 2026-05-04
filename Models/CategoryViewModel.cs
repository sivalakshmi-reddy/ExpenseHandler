using System.ComponentModel.DataAnnotations;

namespace ExpenseHandler.Models
{
    public class CategoryViewModel
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
