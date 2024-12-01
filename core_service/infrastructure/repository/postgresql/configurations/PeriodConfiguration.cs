using core_service.domain.valueobjects;
using core_service.domain.valueobjects.enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace core_service.infrastructure.repository.postgresql.configurations;

public class PeriodConfiguration : IEntityTypeConfiguration<Period>
{
    public void Configure(EntityTypeBuilder<Period> builder)
    {
        builder.ToTable("periods");
        
        builder.HasKey(p => p.Id);

        builder.Property(b => b.UserId).HasColumnName("user_id").IsRequired();
        builder.Property(p => p.Value).HasColumnName("count");
        builder
            .Property(p => p.TypePeriod)
            .HasConversion(v => v.ToString(),
                v => (TypePeriod)Enum.Parse(typeof(TypePeriod), v)).HasColumnName("type");
        
        builder.Property(c => c.CreatedAt).HasColumnName("created_at").IsRequired();
        builder.Property(c => c.UpdatedAt).HasColumnName("updated_at");
        builder.Property(c => c.DeletedAt).HasColumnName("deleted_at");
    }
}