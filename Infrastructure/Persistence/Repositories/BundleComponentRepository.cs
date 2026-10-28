using Microsoft.EntityFrameworkCore;
using CornerPlate.Domain.Entities;
using CornerPlate.Application.Abstractions.Persistence;
using CornerPlate.Infrastructure.Persistence.Helpers;

namespace CornerPlate.Infrastructure.Persistence.Repositories;

public sealed class BundleComponentRepository : IBundleComponentRepository
{
    private readonly AppDbContext _db;

    public BundleComponentRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<BundleComponent?> GetAsync(
        Guid bundleProductId,
        Guid componentProductId,
        CancellationToken ct = default)
        => await _db.BundleComponents
            .SingleOrDefaultAsync(
                bc => bc.BundleProductId == bundleProductId 
                    && bc.ComponentProductId == componentProductId
                , ct);

    public async Task<IReadOnlyList<BundleComponent>> GetByBundleIdAsync(
        Guid bundleProductId,
        CancellationToken ct = default)
        => await _db.BundleComponents
            .AsNoTracking()
            .Include(bc => bc.ComponentProduct)
            .Where(bc => bc.BundleProductId == bundleProductId)
            .OrderBy(bc => bc.SortOrder)
            .ToListAsync(ct);

    public void Add(BundleComponent bundleComponent)
    {
        _db.BundleComponents.Add(bundleComponent);
    }

    public void Remove(BundleComponent bundleComponent)
    {
        _db.BundleComponents.Remove(bundleComponent);
    }
}