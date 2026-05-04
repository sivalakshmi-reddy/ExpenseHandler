using System;
using System.Collections.Generic;
using System.Linq;

namespace ExpenseHandler.Models
{
    public class ReportViewModel
    {
        public Guid Id { get; set; }
        public string EmployeeName { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? SubmittedAt { get; set; }
        public string ManagerComment { get; set; }

        public List<ReportItemViewModel> Items { get; set; } = new List<ReportItemViewModel>();

        public decimal TotalAmount => Items?.Sum(i => i.Amount) ?? 0m;
    }
}
