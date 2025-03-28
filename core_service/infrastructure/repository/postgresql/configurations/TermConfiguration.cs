using core_service.domain.models.valueobjects;
using core_service.domain.models.valueobjects.enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace core_service.infrastructure.repository.postgresql.configurations;

public class TermConfiguration : IEntityTypeConfiguration<Term>
{
    public void Configure(EntityTypeBuilder<Term> builder)
    {
        builder.ToTable("terms");
        
        builder.HasKey(t => t.Id);
        
        builder.Property(t => t.Id).HasDefaultValue(Guid.NewGuid());
        builder.Property(t => t.UserId).HasColumnName("user_id").IsRequired();
        builder
            .Property(t => t.Unit)
            .HasConversion<int>()
            .HasColumnName("unit");
        builder.Property(t => t.CountUnits).HasColumnName("count_units");
        
        builder.Property(c => c.CreatedAt).HasColumnName("created_at").IsRequired().HasDefaultValue(DateTime.UtcNow);;
        builder.Property(c => c.UpdatedAt).HasColumnName("updated_at");
        builder.Property(c => c.DeletedAt).HasColumnName("deleted_at");
    }
}
