using core_service.domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace core_service.infrastructure.repository.postgresql.configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("categories");
        builder.HasKey(c => c.Id);
        builder.ComplexProperty(c => c.Name, nameBuilder =>
        {
            nameBuilder.Property(ic => ic.Value).HasMaxLength(100).HasColumnName("name");
        });
        builder.ComplexProperty(c => c.Color, colorBuilder =>
        {
            colorBuilder.Property(c => c.Value).HasMaxLength(9).HasColumnName("color");
        });
        builder.HasMany(c => c.SubCategories).WithOne();
        
        builder.Property(c => c.CreatedAt).HasColumnName("created_at").IsRequired();
        builder.Property(c => c.UpdatedAt).HasColumnName("updated_at");
        builder.Property(c => c.DeletedAt).HasColumnName("deleted_at");
        
        //TODO: Добавить начальные значения
    }
}