using core_service.domain.valueobjects;
using core_service.domain.valueobjects.enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace core_service.infrastructure.repository.postgresql.configurations;

public class TermConfiguration : IEntityTypeConfiguration<Term>
{
    public void Configure(EntityTypeBuilder<Term> builder)
    {
        builder.ToTable("terms");
        
        builder.HasKey(t => t.Id);
        
        builder.Property(b => b.UserId).HasColumnName("user_id").IsRequired();
        builder
            .Property(t => t.Unit)
            .HasConversion(v => v.ToString(),
                v => (UnitTerm)Enum.Parse(typeof(UnitTerm), v)).HasColumnName("unit");
        builder.Property(t => t.CountUnits).HasColumnName("count_units");
        
        builder.Property(c => c.CreatedAt).HasColumnName("created_at").IsRequired();
        builder.Property(c => c.UpdatedAt).HasColumnName("updated_at");
        builder.Property(c => c.DeletedAt).HasColumnName("deleted_at");
    }
}