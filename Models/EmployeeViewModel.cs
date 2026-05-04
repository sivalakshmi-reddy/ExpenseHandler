using System;
using System.ComponentModel.DataAnnotations;

namespace ExpenseHandler.Models
{
    public class EmployeeViewModel
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        // Store manager reference (optional)
        public Guid? ManagerId { get; set; }

        // Only for display (not filled in form)
        public string? ManagerName { get; set; }
    }
}
