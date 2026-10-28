using Microsoft.EntityFrameworkCore;
using CornerPlate.Domain.Entities;
using CornerPlate.Application.Abstractions.Persistence;

namespace CornerPlate.Infrastructure.Persistence.Repositories;

public class MenuProductRepository : IMenuProductRepository
{
    private readonly AppDbContext _db;

    public MenuProductRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<MenuProduct?> GetByIdAsync(
        Guid id,
        CancellationToken ct = default)
        => await _db.MenuProducts
            .Include(p => p.Category)
            .FirstOrDefaultAsync(mp => mp.Id == id, ct);

    public async Task<IReadOnlyList<MenuProduct>> GetAllAsync(
        CancellationToken ct)
        => await _db.MenuProducts
            .AsNoTracking()
            .Include(p => p.Category)
            .OrderBy(p => p.Name)
            .ToListAsync(ct);

    public void Add(MenuProduct menuProduct)
    {
        _db.MenuProducts.Add(menuProduct);
    }
}