using CornerPlate.Domain.Entities;

namespace CornerPlate.Application.Abstractions.Persistence;

public interface IMenuCategoryRepository
{
    public Task<bool> ExistsByIdAsync(
        Guid id,
        CancellationToken ct = default);

    public Task<bool> ExistsByNameAsync(
        string name,
        CancellationToken ct = default);

    public Task<MenuCategory?> GetByIdAsync(
        Guid id,
        CancellationToken ct = default);

    public Task<IReadOnlyList<MenuCategory>> GetAllAsync(
        CancellationToken ct = default);

    public Task<IReadOnlyList<MenuCategory>> GetAllWithProductsAsync(
        CancellationToken ct = default);

    public void Add(MenuCategory category);
}