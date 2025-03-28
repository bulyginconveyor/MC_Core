using core_service.domain;
using core_service.domain.models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace core_service.infrastructure.repository.postgresql.configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("categories");
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).IsRequired().HasDefaultValue(Guid.NewGuid());
        builder.ComplexProperty(c => c.Name, nameBuilder =>
        {
            nameBuilder.Property(ic => ic.Value).HasMaxLength(100).HasColumnName("name");
        });
        builder.ComplexProperty(c => c.Color, colorBuilder =>
        {
            colorBuilder.Property(c => c.Value).HasMaxLength(9).HasColumnName("color");
        });
        builder.HasMany(c => c.SubCategories).WithOne().HasForeignKey("category_id");;

        builder.Property(c => c.CreatedAt).HasColumnName("created_at").IsRequired().HasDefaultValue(DateTime.UtcNow);
        builder.Property(c => c.UpdatedAt).HasColumnName("updated_at");
        builder.Property(c => c.DeletedAt).HasColumnName("deleted_at");
    }
}
