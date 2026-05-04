using System;
using System.ComponentModel.DataAnnotations;

namespace ExpenseHandler.Models
{
    public class ReportItemViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Category is required")]
        public Guid? CategoryId { get; set; }   // ✅ Nullable Guid for dropdown binding

        public string CategoryName { get; set; } // display only

        [Required]
        public DateTime SpendDate { get; set; } = DateTime.Today;

        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Amount must be greater than 0")]
        public decimal Amount { get; set; }

        public string Description { get; set; }
    }
}
