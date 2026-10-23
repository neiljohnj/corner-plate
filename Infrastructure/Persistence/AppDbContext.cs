using CornerPlate.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CornerPlate.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    // DbSets here
    public DbSet<MenuCategory> MenuCategories => Set<MenuCategory>();
    public DbSet<MenuProduct> MenuProducts => Set<MenuProduct>();
    public DbSet<BundleComponent> BundleComponents => Set<BundleComponent>();
    public DbSet<ProductChoiceGroup> ProductChoiceGroups => Set<ProductChoiceGroup>();
    public DbSet<ProductChoiceOption> ProductChoiceOptions => Set<ProductChoiceOption>();


    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(AppDbContext).Assembly);
    }
}