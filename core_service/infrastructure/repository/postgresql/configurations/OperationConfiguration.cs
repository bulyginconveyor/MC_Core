using core_service.domain;
using core_service.domain.models;
using core_service.domain.models.enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace core_service.infrastructure.repository.postgresql.configurations;

public class OperationConfiguration : IEntityTypeConfiguration<Operation>
{
    public void Configure(EntityTypeBuilder<Operation> builder)
    {
        builder.ToTable("operations");
        
        builder.HasKey(o => o.Id);

        builder.Property(o => o.Id).HasDefaultValue(Guid.NewGuid());
        builder.Property(b => b.UserId).HasColumnName("user_id").IsRequired();
        builder.ComplexProperty(o => o.Name, nameBuilder =>
        {
            nameBuilder
                .Property(n => n.Value)
                .HasColumnName("name")
                .HasMaxLength(100);
        });
        builder.Property(o => o.Date).HasColumnName("date");
        builder.ComplexProperty(o => o.Amount, amountBuilder =>
        {
            amountBuilder
                .Property(a => a.Value)
                .HasColumnName("amount");
        });

        builder
            .HasOne(o => o.Period)
            .WithMany().HasForeignKey("period_id");
        builder
            .HasOne(o => o.Category)
            .WithMany().HasForeignKey("category_id");
        builder
            .HasOne(o => o.CreditBankAccount)
            .WithMany().HasForeignKey("credit_bank_account_id");
        builder
            .HasOne(o => o.DebetBankAccount)
            .WithMany().HasForeignKey("debet_bank_account_id");

        builder
            .Property(c => c.Status)
            .HasConversion<int>()
            .HasColumnName("status");
        
        builder.Property(c => c.CreatedAt).HasColumnName("created_at").IsRequired().HasDefaultValue(DateTime.UtcNow);;
        builder.Property(c => c.UpdatedAt).HasColumnName("updated_at");
        builder.Property(c => c.DeletedAt).HasColumnName("deleted_at");
    }
}
