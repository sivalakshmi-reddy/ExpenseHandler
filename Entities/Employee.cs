using System;

namespace ExpenseHandler.Entities
{
    public class Employee
    {
        public Guid Id { get; set; }     
        public string Name { get; set; }
        public string Email { get; set; }

        public Guid? ManagerId { get; set; }
        public Employee Manager { get; set; }

        public List<ExpenseReport> ExpenseReports { get; set; }
    }
}
