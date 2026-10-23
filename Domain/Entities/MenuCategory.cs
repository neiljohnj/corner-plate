using CornerPlate.Domain.Common;

namespace CornerPlate.Domain.Entities;

public class MenuCategory
{
    private readonly List<MenuProduct> _products = new();

    public Guid Id { get; private set; }
    public string Name { get; private set; } = null!;
    public int SortOrder { get; private set; }
    public bool IsActive { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public IReadOnlyCollection<MenuProduct> Products
        => _products.AsReadOnly();

    // Required by EF Core
    private MenuCategory() { }

    public MenuCategory(string name, int sortOrder = 0)
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.UtcNow;
        IsActive = true;

        Rename(name);
        ChangeSortOrder(sortOrder);
    }

    public void Rename(string name)
    {
        Name = Guard.RequiredText(name, 100, nameof(name));
    }

    public void ChangeSortOrder(int sortOrder)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(
            sortOrder,
            nameof(sortOrder));

        SortOrder = sortOrder;
    }

    public void Activate()
    {
        IsActive = true;
    }

    public void Deactivate()
    {
        IsActive = false;
    }
}
