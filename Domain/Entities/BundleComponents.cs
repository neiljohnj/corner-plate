namespace CornerPlate.Domain.Entities;

public class BundleComponent
{
    // Required by EF Core for materialization.
    private BundleComponent() { }

    public Guid BundleProductId { get; private set; }
    public MenuProduct BundleProduct { get; private set; } = null!;

    public Guid ComponentProductId { get; private set; }
    public MenuProduct ComponentProduct { get; private set; } = null!;

    public int Quantity { get; private set; }
    public int SortOrder { get; private set; }

    public BundleComponent(
        Guid bundleProductId,
        Guid componentProductId,
        int quantity = 1,
        int sortOrder = 0)
    {
        if (bundleProductId == Guid.Empty)
            throw new ArgumentException(
                "Bundle product ID is required.",
                nameof(bundleProductId));

        if (componentProductId == Guid.Empty)
            throw new ArgumentException(
                "Component product ID is required.",
                nameof(componentProductId));

        if (bundleProductId == componentProductId)
            throw new InvalidOperationException(
                "A bundle cannot contain itself.");

        BundleProductId = bundleProductId;
        ComponentProductId = componentProductId;

        ChangeQuantity(quantity);
        ChangeSortOrder(sortOrder);
    }

    public void ChangeQuantity(int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentOutOfRangeException(
                nameof(quantity),
                "Quantity must be greater than zero.");

        Quantity = quantity;
    }

    public void ChangeSortOrder(int sortOrder)
    {
        if (sortOrder < 0)
            throw new ArgumentOutOfRangeException(
                nameof(sortOrder),
                "Sort order cannot be negative.");

        SortOrder = sortOrder;
    }
}
