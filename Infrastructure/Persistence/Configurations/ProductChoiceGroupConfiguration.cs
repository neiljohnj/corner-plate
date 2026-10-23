using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CornerPlate.Domain.Entities;

namespace CornerPlate.Infrastructure.Persistence.Configurations;

public class ProductChoiceGroupConfiguration: IEntityTypeConfiguration<ProductChoiceGroup>
{
    public void Configure(EntityTypeBuilder<ProductChoiceGroup> builder)
    {
        builder.ToTable("product_choice_groups", table =>
        {
            // Setting up Check Constraints
            table.HasCheckConstraint(
                "ck_product_choice_group_name_not_empty",
                "btrim(name) <> ''");

            table.HasCheckConstraint(
                "ck_product_choice_group_type_valid",
                "group_type IN ('Choice', 'Substitution', 'AddOn')");

            table.HasCheckConstraint(
                "ck_product_choice_group_min_selections_nonnegative",
                "min_selections >= 0");
            
            table.HasCheckConstraint(
                "ck_product_choice_group_max_selections_positive",
                "max_selections > 0");
            
            table.HasCheckConstraint(
                "ck_product_choice_group_min_less_than_or_equal_to_max",
                "min_selections <= max_selections");
            
            table.HasCheckConstraint(
                "ck_product_choice_group_nonnegative_sort_order",
                "sort_order >= 0");
        });

        // Primary Key
        builder.HasKey(pc => pc.Id);
        // Properties
        
        builder.Property(pc => pc.MenuProductId);
        
        builder.Property(pc => pc.Name)
            .HasMaxLength(100)
            .IsRequired();
        
        builder.Property(pc => pc.GroupType)
            .HasConversion<string>()
            .HasMaxLength(20)
            .IsRequired();
        
        builder.Property(pc => pc.MinSelections)
            .HasDefaultValue(0);
        
        builder.Property(pc => pc.MaxSelections)
            .IsRequired();
        
        builder.Property(pc => pc.SortOrder)
            .HasDefaultValue(0);
        
        builder.Property(pc => pc.IsActive)
            .HasDefaultValue(true);

        // Foreign Key Relationships
        builder.HasOne(pc => pc.MenuProduct)
            .WithMany(mp => mp.ProductChoiceGroups)
            .HasForeignKey(pc => pc.MenuProductId)
            .OnDelete(DeleteBehavior.Restrict);

        // Indexes
        builder.HasIndex(pcg => new { pcg.MenuProductId, pcg.Name })
            .IsUnique();
    }
}