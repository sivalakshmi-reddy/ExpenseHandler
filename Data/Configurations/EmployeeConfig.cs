using ExpenseHandler.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpenseHandler.Data.Configurations
{
    public class EmployeeConfig : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable("Employees");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(e => e.Email)
                   .IsRequired()
                   .HasMaxLength(150);

            // Self-referencing relationship (ManagerId → Employee.Id)
            builder.HasOne(e => e.Manager)
                   .WithMany()
                   .HasForeignKey(e => e.ManagerId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
