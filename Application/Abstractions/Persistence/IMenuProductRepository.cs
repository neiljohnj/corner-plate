using CornerPlate.Domain.Entities;

namespace CornerPlate.Application.Abstractions.Persistence;

public interface IMenuProductRepository
{
    public void Add(MenuProduct menuProduct);
    public Task<MenuProduct?> GetByIdAsync(Guid id, CancellationToken ct);
    public Task<IReadOnlyList<MenuProduct>> GetAllAsync(CancellationToken ct);
}