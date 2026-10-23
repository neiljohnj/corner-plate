using CornerPlate.Domain.Common;
using CornerPlate.Domain.Enums;

namespace CornerPlate.Domain.Entities;
public class MenuProduct
{
    public Guid Id { get; private set; }
    public Guid CategoryId { get; private set; }
    public MenuCategory Category { get; private set; } = null!;

    public string Name { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public ProductType ProductType { get; private set; }
    public decimal CurrentPrice { get; private set; }
    public string TaxCode { get; private set; } = string.Empty;

    public bool IsAvailable { get; private set; }
    public bool IsActive { get; private set; }

    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    // Collection Navigations
    private readonly List<BundleComponent> _bundleComponents = [];
    private readonly List<BundleComponent> _usedInBundles = [];
    private readonly List<ProductChoiceGroup> _productChoiceGroups = [];
    private readonly List<ProductChoiceOption> _usedInChoiceOptions = [];

    // Public collections to expose the private lists as read-only
    public IReadOnlyCollection<BundleComponent> BundleComponents => _bundleComponents;

    public IReadOnlyCollection<BundleComponent> UsedInBundles => _usedInBundles;
    public IReadOnlyCollection<ProductChoiceGroup> ProductChoiceGroups => _productChoiceGroups;
    public IReadOnlyCollection<ProductChoiceOption> UsedInChoiceOptions => _usedInChoiceOptions;

    // Required by EF Core.
    private MenuProduct() { }

    public MenuProduct(
        Guid categoryId,
        string name,
        string? description,
        ProductType productType,
        decimal currentPrice,
        string taxCode)
    {
        if (categoryId == Guid.Empty)
            throw new ArgumentException(
                "Category ID is required.",
                nameof(categoryId));

        if (!Enum.IsDefined(productType))
            throw new ArgumentOutOfRangeException(
                nameof(productType),
                "Invalid product type.");

        Id = Guid.NewGuid();
        CategoryId = categoryId;
        ProductType = productType;

        IsAvailable = true;
        IsActive = true;

        Rename(name);
        ChangeDescription(description);
        ChangeCurrentPrice(currentPrice);
        ChangeTaxCode(taxCode);

        CreatedAt = DateTime.UtcNow;
        UpdatedAt = CreatedAt;
    }

    public void Rename(string newName)
    {
        Name = Guard.RequiredText(newName, 160, nameof(newName));
        Touch();
    }

    public void ChangeDescription(string? description)
    {
        Description = string.IsNullOrWhiteSpace(description)
            ? null
            : description.Trim();

        Touch();
    }

    public void ChangeProductType(ProductType productType)
    {
        if (!Enum.IsDefined(productType))
            throw new ArgumentOutOfRangeException(
                nameof(productType),
                "Invalid product type choice given.");

        ProductType = productType;
        Touch();
    }

    public void ChangeCurrentPrice(decimal price)
    {
        if (price < 0 || price > 9999999999.99m)
            throw new ArgumentOutOfRangeException(
                nameof(price),
                "Price is outside the allowed range.");

        if (decimal.Round(price, 2) != price)
            throw new ArgumentException(
                "Price cannot have more than two decimal places.",
                nameof(price));

        CurrentPrice = price;
        Touch();
    }

    public void ChangeTaxCode(string taxCode)
    {

        TaxCode = Guard.RequiredText(taxCode, 32, nameof(taxCode));
        Touch();
    }

    public void MakeAvailable()
    {
        IsAvailable = true;
        Touch();
    }

    public void MakeUnavailable()
    {
        IsAvailable = false;
        Touch();
    }

    public void Activate()
    {
        IsActive = true;
        Touch();
    }

    public void Deactivate()
    {
        IsActive = false;
        Touch();
    }

    private void Touch()
    {
        UpdatedAt = DateTime.UtcNow;
    }
}
