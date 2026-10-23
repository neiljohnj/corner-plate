using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CornerPlate.Domain.Entities;

namespace CornerPlate.Infrastructure.Persistence.Configurations;

public class BundleComponentConfiguration : IEntityTypeConfiguration<BundleComponent>
{
    public void Configure(EntityTypeBuilder<BundleComponent> builder)
    {
        builder.ToTable("bundle_components", table =>
        {
            // Setting up Check Constraints
            table.HasCheckConstraint(
                "ck_bundle_component_positive_quantity",
                "quantity > 0");

            table.HasCheckConstraint(
                "ck_bundle_component_bundle_product_not_equal_to_component_product_id",
                "bundle_product_id <> component_product_id");

            table.HasCheckConstraint(
                "ck_bundle_component_nonnegative_sort_order",
                "sort_order >= 0");

            // I don't know how to set up a constraint on the foreign key...
            // I'm referring to the second argument of a Check Constraint...

        });

        // Primary Key
        builder.HasKey(bc => new
        {
            bc.BundleProductId,
            bc.ComponentProductId
        });

        // Properties

        builder.Property(bc => bc.BundleProductId);

        builder.Property(bc => bc.ComponentProductId);

        builder.Property(bc => bc.Quantity)
            .HasDefaultValue(1);

        builder.Property(bc => bc.SortOrder)
            .HasDefaultValue(0);

        // Foreign Key Relationships

        // Foreign Key: Parent Bundle
        builder.HasOne(bc => bc.BundleProduct)
            .WithMany(mp => mp.BundleComponents)
            .HasForeignKey(bc => bc.BundleProductId)
            .OnDelete(DeleteBehavior.Restrict);

        // Foreign Key: Component Product
        builder.HasOne(bc => bc.ComponentProduct)
            .WithMany(mp => mp.UsedInBundles)
            .HasForeignKey(bc => bc.ComponentProductId)
            .OnDelete(DeleteBehavior.Restrict);

        // Indexes
        builder.HasIndex(bc => bc.ComponentProductId)
            .HasDatabaseName(
                "ix_bundle_components_component_product_id");
    }
}