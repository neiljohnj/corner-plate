using CornerPlate.Application.Abstractions.Persistence;
using CornerPlate.Application.Menus.DTOs;
using CornerPlate.Domain.Entities;
using CornerPlate.Domain.Enums;

namespace CornerPlate.Application.Menus.Services;

public sealed class BundleConfigurationService
{
    private readonly IBundleComponentRepository _bundles;
    private readonly IMenuProductRepository _products;
    private readonly IUnitOfWork _unitOfWork;

    public BundleConfigurationService(
        IBundleComponentRepository bundles,
        IMenuProductRepository products,
        IUnitOfWork unitOfWork)
    {
        _bundles = bundles;
        _products = products;
        _unitOfWork = unitOfWork;
    }

    public static BundleComponentDTO ToDto(BundleComponent component)
        => ToDto(component, component.ComponentProduct.Name);

    public static BundleComponentDTO ToDto(BundleComponent component, string cpName)
        => new(
            component.BundleProductId,
            component.ComponentProductId,
            cpName,
            component.Quantity,
            component.SortOrder);

    public async Task<BundleComponentDTO> AddBundleComponentAsync(
        AddBundleComponentRequest request,
        CancellationToken ct = default)
    {
        // Check if product ID exists...
        var bundleProduct = await _products.GetByIdAsync(request.BundleProductId, ct) 
            ?? throw new InvalidOperationException(
                $"Bundle product {request.BundleProductId} does not exist!");

        if (bundleProduct.ProductType != ProductType.Bundle)
            throw new InvalidOperationException(
                $"Selected product {bundleProduct.Name} is not of bundle type!");

        var componentProduct = await _products.GetByIdAsync(request.ComponentProductId, ct)
            ?? throw new InvalidOperationException(
                $"Component product {request.ComponentProductId} does not exist!");

        if (componentProduct.ProductType != ProductType.Single)
            throw new InvalidOperationException(
                $"Selected product {componentProduct.Name} is not of single type!");

        // Check if another component already exists...
        if (await _bundles.GetAsync(
                request.BundleProductId,
                request.ComponentProductId,
                ct) is not null)
            throw new InvalidOperationException(
                "This product is already part of the bundle.");

        var bundleComponent = new BundleComponent(
            bundleProduct.Id,
            componentProduct.Id,
            request.Quantity,
            request.SortOrder);

        _bundles.Add(bundleComponent);
        await _unitOfWork.SaveChangesAsync(ct);

        return ToDto(bundleComponent, componentProduct.Name);
    }

    public async Task<IReadOnlyList<BundleComponentDTO>> GetComponentsAsync(
        Guid bundleProductId,
        CancellationToken ct = default)
    {
        var components = await _bundles.GetByBundleIdAsync(bundleProductId, ct);

        return [.. components.Select(ToDto) ];
    }

    public async Task<bool> ChangeComponentQuantityAsync(
        Guid bundleProductId,
        Guid componentProductId,
        int newQuantity,
        CancellationToken ct = default)
    {
        var bundleComponent = await _bundles.GetAsync(
            bundleProductId, componentProductId, ct);

        if (bundleComponent is null)
            return false;

        bundleComponent.ChangeQuantity(newQuantity);
        await _unitOfWork.SaveChangesAsync(ct);

        return true;
    }

    public async Task<bool> ChangeSortOrderAsync(
        Guid bundleProductId,
        Guid componentProductId,
        int sortOrder,
        CancellationToken ct = default)
    {
        var bundleComponent = await _bundles.GetAsync(
            bundleProductId, componentProductId, ct);

        if (bundleComponent is null)
            return false;

        bundleComponent.ChangeSortOrder(sortOrder);
        await _unitOfWork.SaveChangesAsync(ct);

        return true;
    }
}