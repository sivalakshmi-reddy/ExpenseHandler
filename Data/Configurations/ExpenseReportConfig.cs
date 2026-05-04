using ExpenseHandler.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpenseHandler.Data.Configurations
{
    public class ExpenseReportConfig : IEntityTypeConfiguration<ExpenseReport>
    {
        public void Configure(EntityTypeBuilder<ExpenseReport> builder)
        {
            builder.ToTable("ExpenseReports");

            builder.HasKey(r => r.Id);

            builder.Property(r => r.CreatedAt)
                   .IsRequired();

            builder.Property(r => r.Status)
                   .IsRequired();

            builder.Property(r => r.ManagerComment)
                   .HasMaxLength(500);

            // Relationship: Report belongs to Employee
            builder.HasOne(r => r.Employee)
                   .WithMany(e => e.ExpenseReports)
                   .HasForeignKey(r => r.EmployeeId);
        }
    }
}
