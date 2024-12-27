using core_service.domain;
using core_service.domain.models;
using core_service.domain.models.enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace core_service.infrastructure.repository.postgresql.configurations;

public class ActiveBankAccountConfiguration : IEntityTypeConfiguration<ActiveBankAccount>
{
    public void Configure(EntityTypeBuilder<ActiveBankAccount> builder)
    {
        builder.ToTable("active_bank_accounts");
        
        builder.ComplexProperty(a => a.BuyPrice, buyPriceBuilder =>
        {
            buyPriceBuilder.Property(b => b.Value).HasColumnName("buy_price");
        });
        builder.Property(a => a.BuyDate).HasColumnName("buy_date");
        builder.ComplexProperty(a => a.PhotoUrl, photoUrlBuilder =>
        {
            photoUrlBuilder.Property(p => p.Url).HasColumnName("photo_url");
        });
        
        builder
            .Property(b => b.TypeActive)
            .HasConversion(v => v.ToString(),
                v => (TypeActiveBankAccount)Enum.Parse(typeof(TypeActiveBankAccount), v)).HasColumnName("type_active");
        
        
    }
}