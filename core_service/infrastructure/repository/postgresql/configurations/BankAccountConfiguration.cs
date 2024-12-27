using core_service.domain;
using core_service.domain.models;
using core_service.domain.models.enums;
using core_service.services.GuidGenerator;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace core_service.infrastructure.repository.postgresql.configurations;

public class BankAccountConfiguration : IEntityTypeConfiguration<BankAccount>
{
    public void Configure(EntityTypeBuilder<BankAccount> builder)
    {
        builder.UseTptMappingStrategy();
        
        builder.ToTable("bank_accounts");
        builder.HasKey(b => b.Id);
        
        builder.Property(b => b.Id).HasDefaultValue(GuidGenerator.GenerateByBytes());
        builder.Property(b => b.UserId).HasColumnName("user_id").IsRequired();
        builder.ComplexProperty(b => b.Name, nameBuilder =>
        {
            nameBuilder.Property(n => n.Value).HasColumnName("name").HasMaxLength(100);
        });
        builder.ComplexProperty(b => b.Color, colorBuilder =>
        {
            colorBuilder.Property(c => c.Value).HasColumnName("color").HasMaxLength(9);
        });
        builder.ComplexProperty(b => b.Balance, balanceBuilder =>
        {
            balanceBuilder.Property(b => b.Value).HasColumnName("balance");
            balanceBuilder.Property(b => b.isMaybeNegative).HasColumnName("is_maybe_negative");
        });
        
        builder
            .Property(b => b.Type)
            .HasConversion(v => v.ToString(),
                v => (TypeBankAccount)Enum.Parse(typeof(TypeBankAccount), v)).HasColumnName("type");
        
        builder.Property(c => c.CreatedAt).HasColumnName("created_at").IsRequired().HasDefaultValue(DateTime.UtcNow);;
        builder.Property(c => c.UpdatedAt).HasColumnName("updated_at");
        builder.Property(c => c.DeletedAt).HasColumnName("deleted_at");
        
        builder.HasOne(b => b.Currency).WithMany();
    }
}