using CornerPlate.Domain.Enums;

namespace CornerPlate.Application.Menus.DTOs;

public sealed record CreateMenuProductRequest(
    Guid CategoryId,
    string Name,
    string? Description,
    ProductType ProductType,
    decimal CurrentPrice,
    string TaxCode,
    bool IsAvailable = true,
    bool IsActive = true
    );

public sealed record MenuCategoryProductDTO(
    Guid Id,
    string Name,
    string? Description,
    decimal CurrentPrice,
    ProductType ProductType,
    bool IsAvailable,
    bool IsActive);

public sealed record MenuProductDTO(
    Guid Id,
    Guid CategoryId,
    string Name,
    string CategoryName,
    string? Description,
    ProductType ProductType,
    decimal CurrentPrice,
    string TaxCode,
    bool IsAvailable,
    bool IsActive
    );