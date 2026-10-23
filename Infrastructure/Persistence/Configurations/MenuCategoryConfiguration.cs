using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CornerPlate.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CornerPlate.Infrastructure.Persistence.Configurations;

public class MenuCategoryConfiguration : IEntityTypeConfiguration<MenuCategory>
{
    public void Configure(EntityTypeBuilder<MenuCategory> builder)
    {
        builder.ToTable("menu_categories", table =>
        {
            table.HasCheckConstraint(
                "ck_menu_category_nonnegative_sort_order",
                "sort_order >= 0"
            );
        });

        builder.HasKey(mc => mc.Id);

        // Properties

        builder.Property(mc => mc.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(mc => mc.SortOrder)
            .HasColumnName("sort_order")
            .HasDefaultValue(0);

        builder.Property(mc => mc.IsActive)
            .HasDefaultValue(true);

        builder.Property(mc => mc.CreatedAt)
            .HasColumnName("created_at")
            .HasColumnType("timestamp with time zone")
            .HasDefaultValueSql("now()");

        // Indexes

        builder.HasIndex(mc => mc.Name)
            .IsUnique();
    }
}