using System;

namespace ExpenseHandler.Entities
{
    public class ExpenseItem
    {
        public Guid Id { get; set; }               

        public Guid ReportId { get; set; }        
        public ExpenseReport Report { get; set; }

        public Guid CategoryId { get; set; }       
        public Category Category { get; set; }

        public DateTime SpendDate { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
    }
}
