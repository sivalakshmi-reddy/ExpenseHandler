using System;
using ExpenseHandler.Enums;

namespace ExpenseHandler.Entities
{
    public class ExpenseReport
    {
        public Guid Id { get; set; }               
        public Guid EmployeeId { get; set; }      
        public Employee Employee { get; set; }
        public ReportStatus Status { get; set; }  
        public DateTime CreatedAt { get; set; }
        public DateTime? SubmittedAt { get; set; }
        public string? ManagerComment { get; set; }   

        public List<ExpenseItem> Items { get; set; }
    }
}
