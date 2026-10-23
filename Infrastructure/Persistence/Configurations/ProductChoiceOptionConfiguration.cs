using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CornerPlate.Domain.Entities;

namespace CornerPlate.Infrastructure.Persistence.Configurations;

public class ProductChoiceOptionConfiguration : IEntityTypeConfiguration<ProductChoiceOption>
{
    public void Configure(EntityTypeBuilder<ProductChoiceOption> builder)
    {
        builder.ToTable("product_choice_options", table =>
        {
            // Setting up constraints

            table.HasCheckConstraint(
                "ck_product_choice_option_label_not_empty",
                "btrim(label) <> ''");

            table.HasCheckConstraint(
                "ck_product_choice_option_nonnegative_price_adjustment",
                "price_adjustment >= 0");

            table.HasCheckConstraint(
                "ck_product_choice_option_positive_max_quantity",
                "max_quantity > 0");

            table.HasCheckConstraint(
                "ck_product_choice_option_nonnegative_sort_order",
                "sort_order >= 0");
        });

        // Primary Key
        builder.HasKey(pco => pco.Id);

        // Properties
        builder.Property(pco => pco.ChoiceGroupId);

        builder.Property(pco => pco.ComponentProductId);

        builder.Property(pco => pco.Label)
            .HasMaxLength(120)
            .IsRequired();

        builder.Property(pco => pco.PriceAdjustment)
            .HasPrecision(12, 2)
            .HasDefaultValue(0);

        builder.Property(pco => pco.MaxQuantity)
            .HasDefaultValue(1);

        builder.Property(pco => pco.IsDefault)
            .HasDefaultValue(false);

        builder.Property(pco => pco.SortOrder)
            .HasDefaultValue(0);

        builder.Property(pco => pco.IsActive)
            .HasDefaultValue(true);

        // Foreign Key Relationships
        builder.HasOne(pco => pco.ProductChoiceGroup)
            .WithMany(pcg => pcg.ProductChoiceOptions)
            .HasForeignKey(pco => pco.ChoiceGroupId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(pco => pco.ComponentProduct)
            .WithMany(mp => mp.UsedInChoiceOptions)
            .HasForeignKey(pco => pco.ComponentProductId)
            .OnDelete(DeleteBehavior.Restrict);

        // Setting up Indexes
        builder.HasAlternateKey(pco => new
        {
            pco.ChoiceGroupId,
            pco.Id
        }).HasName("uq_product_choice_options_group_id");

        builder.HasIndex(pco => new
        {
            pco.ChoiceGroupId,
            pco.Label
        }).IsUnique();
    }
}