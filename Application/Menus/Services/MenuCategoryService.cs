using CornerPlate.Application.Abstractions.Persistence;
using CornerPlate.Application.Menus.DTOs;
using CornerPlate.Domain.Entities;

namespace CornerPlate.Application.Menus.Services;

public sealed class MenuCategoryService
{
    private readonly IMenuCategoryRepository _categories;
    private readonly IUnitOfWork _unitOfWork;

    public MenuCategoryService(
        IMenuCategoryRepository categories,
        IUnitOfWork unitOfWork)
    {
        _categories = categories;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> CreateAsync(
        string name,
        int sortOrder = 0,
        CancellationToken ct = default)
    {
        // The Domain validates name and sort order
        var category = new MenuCategory(name, sortOrder);

        // Application level duplicate check
        if (await _categories.ExistsByNameAsync(category.Name, ct))
        {
            throw new InvalidOperationException(
                "A category with this name already exists!");
        }

        _categories.Add(category);

        await _unitOfWork.SaveChangesAsync(ct);

        return category.Id;
    }

    public async Task<bool> RenameAsync(
        Guid id,
        string name,
        CancellationToken ct = default)
    {
        var category = await _categories.GetByIdAsync(id, ct);

        if (category is null)
            return false;

        category.Rename(name);
        await _unitOfWork.SaveChangesAsync(ct);

        return true;
    }

    public async Task<IReadOnlyList<MenuCategoryDTO>> GetAllAsync(
        CancellationToken ct = default)
    {
        var categories = await _categories.GetAllAsync(ct);

        return [.. categories
            .Select(c => new MenuCategoryDTO(
                c.Id,
                c.Name,
                c.SortOrder,
                c.IsActive))];
    }

    public async Task<IReadOnlyList<MenuCategoryWithProductsDTO>> GetAllWithProductsAsync(
        CancellationToken ct = default)
    {
        var categoriesWithProducts = await _categories.GetAllWithProductsAsync(ct);

        return [.. categoriesWithProducts
            .Select(c => new MenuCategoryWithProductsDTO(
                c.Id,
                c.Name,
                c.SortOrder,
                c.IsActive,
                [.. c.Products.Select(p => new MenuCategoryProductDTO(
                    p.Id,
                    p.Name,
                    p.Description,
                    p.CurrentPrice,
                    p.ProductType,
                    p.IsAvailable,
                    p.IsActive
                ))]
        ))];
    }
}