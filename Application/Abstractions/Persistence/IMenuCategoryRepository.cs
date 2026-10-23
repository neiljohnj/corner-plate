using CornerPlate.Domain.Entities;

namespace CornerPlate.Application.Abstractions.Persistence;

public interface IMenuCategoryRepository
{
    Task<bool> ExistsByNameAsync(
        string name,
        CancellationToken ct = default);

    Task<IReadOnlyList<MenuCategory>> GetAllAsync(
        CancellationToken ct = default);

    void Add(MenuCategory category);
}