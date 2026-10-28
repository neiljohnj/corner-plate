namespace CornerPlate.Application.Menus.DTOs;

public sealed record MenuCategoryDTO(
    Guid Id,
    string Name,
    int SortOrder,
    bool IsActive);

public sealed record MenuCategoryWithProductsDTO(
    Guid Id,
    string Name,
    int SortOrder,
    bool IsActive,
    IReadOnlyCollection<MenuCategoryProductDTO> Products
    );