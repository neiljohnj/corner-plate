using CornerPlate.Application.Abstractions.Persistence;
using CornerPlate.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CornerPlate.Infrastructure.Persistence.Repositories;

public sealed class MenuCategoryRepository : IMenuCategoryRepository
{
    private readonly AppDbContext _db;

    public MenuCategoryRepository(AppDbContext db)
    {
        _db = db;
    }

    public Task<bool> ExistsByIdAsync(
        Guid id,
        CancellationToken ct = default)
    {
        return _db.MenuCategories.AnyAsync(
            c => c.Id == id, ct);
    }

    public Task<bool> ExistsByNameAsync(
        string name,
        CancellationToken ct = default)
    {
        var normalized = name.ToLowerInvariant();

        return _db.MenuCategories.AnyAsync(
            c => string.Equals(c.Name.ToLower(), normalized), ct);
    }

    public async Task<MenuCategory?> GetByIdAsync(
        Guid id,
        CancellationToken ct = default)
    {
        return await _db.MenuCategories
            .FindAsync([id], ct);
    }

    public async Task<IReadOnlyList<MenuCategory>> GetAllAsync(
        CancellationToken ct = default)
    {
        return await _db.MenuCategories
            .AsNoTracking()
            .OrderBy(c => c.SortOrder)
            .ThenBy(c => c.Name)
            .ToListAsync(ct);
    }

    public async Task<IReadOnlyList<MenuCategory>> GetAllWithProductsAsync(
        CancellationToken ct = default)
    {
        return await _db.MenuCategories
            .AsNoTracking()
            .Include(mc => mc.Products)
            .OrderBy(mc => mc.SortOrder)
            .ThenBy(mc => mc.Name)
            .ToListAsync(ct);
    }

    public void Add(MenuCategory category)
    {
        _db.MenuCategories.Add(category);
    }
}