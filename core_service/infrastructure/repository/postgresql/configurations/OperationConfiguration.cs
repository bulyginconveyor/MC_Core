using core_service.domain;
using core_service.domain.enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace core_service.infrastructure.repository.postgresql.configurations;

public class OperationConfiguration : IEntityTypeConfiguration<Operation>
{
    public void Configure(EntityTypeBuilder<Operation> builder)
    {
        builder.ToTable("operations");
        
        builder.HasKey(o => o.Id);

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
            .WithMany();
        builder
            .HasOne(o => o.Category)
            .WithMany();
        builder
            .HasOne(o => o.CreditBankAccount)
            .WithMany();
        builder
            .HasOne(o => o.DebetBankAccount)
            .WithMany();

        builder
            .Property(c => c.Status)
            .HasConversion(v => v.ToString(),
                v => (StatusOperation)Enum.Parse(typeof(StatusOperation), v)).HasColumnName("status");
        
        builder.Property(c => c.CreatedAt).HasColumnName("created_at").IsRequired();
        builder.Property(c => c.UpdatedAt).HasColumnName("updated_at");
        builder.Property(c => c.DeletedAt).HasColumnName("deleted_at");
    }
}