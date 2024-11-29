using core_service.domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace core_service.infrastructure.repository.postgresql.configurations;

public class CurrencyConfiguration : IEntityTypeConfiguration<Currency>
{
    public void Configure(EntityTypeBuilder<Currency> builder)
    {
        builder.ToTable("currency");
        builder.ComplexProperty(c => c.IsoCode, isoCodeBuilder =>
        {
            isoCodeBuilder.Property(ic => ic.Value).HasMaxLength(3).HasColumnName("iso_code");
        });
        builder.ComplexProperty(c => c.FullName, nameBuilder =>
        {
            nameBuilder.Property(n => n.Value).HasMaxLength(100).HasColumnName("name");
        });
        builder.ComplexProperty(c => c.ImageUrl, photoUrlBuilder =>
        {
            photoUrlBuilder.Property(pu => pu.Url).HasMaxLength(2048).HasColumnName("image_url");
        });
        
        builder.HasKey(c => c.Id);

        builder.Property(c => c.CreatedAt).HasColumnName("created_at").IsRequired();
        builder.Property(c => c.UpdatedAt).HasColumnName("updated_at");
        builder.Property(c => c.DeletedAt).HasColumnName("deleted_at");
        
        //TODO: Добавить начальные значения
    }
}