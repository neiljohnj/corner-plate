using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CornerPlate.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CornerPlate.Infrastructure.Persistence.Configurations;

public class MenuProductConfiguration: IEntityTypeConfiguration<MenuProduct>
{
    public void Configure(EntityTypeBuilder<MenuProduct> builder)
    {
        // Table and constraints
        builder.ToTable("menu_products", table =>
        {
            table.HasCheckConstraint(
                "ck_menu_product_strict_product_type",
                "product_type IN ('Single','Bundle')");

            table.HasCheckConstraint(
                "ck_menu_product_nonnegative_price",
                "current_price >= 0");

            table.HasCheckConstraint(
                "ck_menu_product_name_not_empty",
                "btrim(name) <> ''");
        });

        // Primary Key
        builder.HasKey(mp => mp.Id);

        // Properties
        builder.Property(mp => mp.Name)
            .HasColumnName("name")
            .HasMaxLength(160)
            .IsRequired();

        builder.Property(mp => mp.Description)
            .HasColumnType("text");

        builder.Property(mp => mp.ProductType)
            .HasConversion<string>()
            .HasMaxLength(12)
            .IsRequired();

        builder.Property(mp => mp.CurrentPrice)
            .HasPrecision(12, 2)
            .IsRequired();

        builder.Property(mp => mp.TaxCode)
            .HasMaxLength(32)
            .IsRequired();

        builder.Property(mp => mp.IsAvailable)
            .HasDefaultValue(true);

        builder.Property(mp => mp.IsActive)
            .HasDefaultValue(true);

        builder.Property(mp => mp.CreatedAt)
            .HasColumnName("created_at")
            .HasColumnType("timestamp with time zone")
            .HasDefaultValueSql("now()");

        builder.Property(mp => mp.UpdatedAt)
            .HasColumnName("updated_at")
            .HasColumnType("timestamp with time zone")
            .HasDefaultValueSql("now()");

        // Relationship
        builder.HasOne(mp => mp.Category)
            .WithMany(mc => mc.Products)
            .HasForeignKey(mp => mp.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);


        // Indexes

        builder.HasIndex(mp => mp.CategoryId);

        builder.HasIndex(mp => new
        {
            mp.CategoryId,
            mp.IsActive,
            mp.IsAvailable
        });

        builder.HasIndex(mp => new
        {
            mp.ProductType,
            mp.IsActive
        });
    }
}