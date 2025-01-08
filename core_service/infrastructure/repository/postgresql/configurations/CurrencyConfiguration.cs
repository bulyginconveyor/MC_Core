using core_service.domain;
using core_service.domain.models;
using core_service.domain.models.valueobjects;
using core_service.services.GuidGenerator;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace core_service.infrastructure.repository.postgresql.configurations;

public class CurrencyConfiguration : IEntityTypeConfiguration<Currency>
{
    public void Configure(EntityTypeBuilder<Currency> builder)
    {
        builder.ToTable("currency");
        
        builder.Property(c => c.Id).HasDefaultValue(GuidGenerator.GenerateByBytes());
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

        builder.Property(c => c.CreatedAt).HasColumnName("created_at").IsRequired().HasDefaultValue(DateTime.UtcNow);;
        builder.Property(c => c.UpdatedAt).HasColumnName("updated_at");
        builder.Property(c => c.DeletedAt).HasColumnName("deleted_at");
        
    }
}
