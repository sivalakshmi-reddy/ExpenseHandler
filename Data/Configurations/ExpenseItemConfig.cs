using ExpenseHandler.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpenseHandler.Data.Configurations
{
    public class ExpenseItemConfig : IEntityTypeConfiguration<ExpenseItem>
    {
        public void Configure(EntityTypeBuilder<ExpenseItem> builder)
        {
            builder.ToTable("ExpenseItems");

            builder.HasKey(i => i.Id);

            builder.Property(i => i.Amount)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");

            builder.Property(i => i.Description)
                   .HasMaxLength(200);

            // Relationship: Item belongs to Report
            builder.HasOne(i => i.Report)
                   .WithMany(r => r.Items)
                   .HasForeignKey(i => i.ReportId);

            // Relationship: Item belongs to Category
            builder.HasOne(i => i.Category)
                   .WithMany(c => c.ExpenseItems)
                   .HasForeignKey(i => i.CategoryId);
        }
    }
}
