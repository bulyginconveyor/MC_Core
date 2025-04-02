using core_service.infrastructure.repository.postgresql.models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace core_service.infrastructure.repository.postgresql.configurations;

public class HiddenCategoryConfiguration : IEntityTypeConfiguration<HiddenCategory>
{
    public void Configure(EntityTypeBuilder<HiddenCategory> builder)
    {
        builder.ToTable("hidden_categories");
        builder.HasKey(e => new { e.CategoryId, e.UserId});
        
        builder.Property(e => e.CategoryId).IsRequired().HasColumnName("category_id");
        builder.Property(e => e.UserId).IsRequired().HasColumnName("user_id");
    }
}
