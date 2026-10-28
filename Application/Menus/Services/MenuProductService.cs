using CornerPlate.Domain.Entities;
using CornerPlate.Application.Abstractions.Persistence;
using CornerPlate.Application.Menus.DTOs;

namespace CornerPlate.Application.Menus.Services;

public sealed class MenuProductService
{
    private readonly IMenuProductRepository _products;
    private readonly IMenuCategoryRepository _categories;
    private readonly IUnitOfWork _unitOfWork;

    public MenuProductService(
        IMenuProductRepository products,
        IMenuCategoryRepository categories,
        IUnitOfWork unitOfWork)
    {
        _products = products;
        _categories = categories;
        _unitOfWork = unitOfWork;
    }

    private static MenuProductDTO ToDto(MenuProduct product)
        => ToDto(product, product.Category.Name);

    private static MenuProductDTO ToDto(MenuProduct product, string categoryName)
        => new(
            product.Id,
            product.CategoryId,
            product.Name,
            categoryName,
            product.Description,
            product.ProductType,
            product.CurrentPrice,
            product.TaxCode,
            product.IsAvailable,
            product.IsActive
        );

    public async Task<MenuProductDTO> CreateAsync(
        CreateMenuProductRequest request,
        CancellationToken ct = default)
    {

        var category = await _categories.GetByIdAsync(request.CategoryId, ct) 
            ?? throw new InvalidOperationException(
                $"The selected category: '{request.CategoryId}', does not exist!");

        MenuProduct menuProduct = new(
            request.CategoryId,
            request.Name,
            request.Description,
            request.ProductType,
            request.CurrentPrice,
            request.TaxCode);

        if (!request.IsActive) menuProduct.Deactivate();
        if (!request.IsAvailable) menuProduct.MakeUnavailable();

        _products.Add(menuProduct);
        await _unitOfWork.SaveChangesAsync(ct);

        return ToDto(menuProduct, category.Name);
    }

    public async Task<MenuProductDTO?> GetByIdAsync(
        Guid id,
        CancellationToken ct = default)
    {
        var product = await _products.GetByIdAsync(id, ct);

        return product is null ? null : ToDto(product);
    }

    public async Task<IReadOnlyList<MenuProductDTO>> GetAllAsync(
        CancellationToken ct = default)
    {
        var products = await _products.GetAllAsync(ct);

        return [.. products.Select(ToDto) ];
    }

    public async Task<bool> ChangeCategoryAsync(
        Guid id,
        Guid categoryId,
        CancellationToken ct = default)
    {
        if (!await _categories.ExistsByIdAsync(categoryId, ct))
            throw new InvalidOperationException(
                $"Category {categoryId} does not exist!");

        var product = await _products.GetByIdAsync(id, ct);

        if (product is null)
            return false;

        product.ChangeCategory(categoryId);
        await _unitOfWork.SaveChangesAsync(ct);

        return true;
    }

    public async Task<bool> ChangePriceAsync(
        Guid id,
        decimal newPrice,
        CancellationToken ct = default)
    {
        var product = await _products.GetByIdAsync(id, ct);

        if (product is null)
            return false;

        product.ChangeCurrentPrice(newPrice);

        await _unitOfWork.SaveChangesAsync(ct);

        return true;
    }

    public async Task<bool> SetAvailabilityAsync(
        Guid id,
        bool availability,
        CancellationToken ct = default)
    {
        var product = await _products.GetByIdAsync(id, ct);

        if (product is null)
            return false;

        if (availability)
            product.MakeAvailable();
        else
            product.MakeUnavailable();

        await _unitOfWork.SaveChangesAsync(ct);

        return true;
    }
}