using core_service.domain.models.valueobjects;
using core_service.domain.models.valueobjects.enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace core_service.infrastructure.repository.postgresql.configurations;

public class PeriodConfiguration : IEntityTypeConfiguration<Period>
{
    public void Configure(EntityTypeBuilder<Period> builder)
    {
        builder.ToTable("periods");
        
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id).HasDefaultValue(Guid.NewGuid());
        builder.Property(b => b.UserId).HasColumnName("user_id").IsRequired();
        builder.Property(p => p.Value).HasColumnName("count");
        builder
            .Property(p => p.TypePeriod)
            .HasConversion<int>()
            .HasColumnName("type");
        
        builder.Property(c => c.CreatedAt).HasColumnName("created_at").IsRequired().HasDefaultValue(DateTime.UtcNow);;
        builder.Property(c => c.UpdatedAt).HasColumnName("updated_at");
        builder.Property(c => c.DeletedAt).HasColumnName("deleted_at");
    }
}
