using CornerPlate.Domain.Entities;

namespace CornerPlate.Application.Abstractions.Persistence;

public interface IBundleComponentRepository
{
    public Task<BundleComponent?> GetAsync(
        Guid bundleProductId,
        Guid componentProductId,
        CancellationToken ct = default);


    public Task<IReadOnlyList<BundleComponent>> GetByBundleIdAsync(
        Guid bundleProductId,
        CancellationToken ct = default);

    public void Add(BundleComponent bundleComponent);

    public void Remove(BundleComponent bundleComponent);
}