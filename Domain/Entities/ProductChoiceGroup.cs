using CornerPlate.Domain.Enums;
using CornerPlate.Domain.Common;

namespace CornerPlate.Domain.Entities;

public class ProductChoiceGroup
{
    public Guid Id { get; private set; }

    public Guid MenuProductId { get; private set; }
    public MenuProduct MenuProduct { get; private set; } = null!;

    public string Name { get; private set; } = null!;
    public ChoiceGroupType GroupType { get; private set; }

    public int MinSelections { get; private set; }
    public int MaxSelections { get; private set; }
    public int SortOrder { get; private set; }

    public bool IsActive { get; private set; }

    // Navigation Links
    private readonly List<ProductChoiceOption> _productChoiceOptions = [];

    // Exposed public references of nav links
    public IReadOnlyCollection<ProductChoiceOption> ProductChoiceOptions => _productChoiceOptions;

    private ProductChoiceGroup() { }

    public ProductChoiceGroup(
        Guid menuProductId,
        string name,
        int maxSelections,
        ChoiceGroupType groupType,
        int minSelections = 0,
        int sortOrder = 0)
    {
        if (menuProductId == Guid.Empty)
            throw new ArgumentException(
                "Menu product ID is required.",
                nameof(menuProductId));

        Id = Guid.NewGuid();
        MenuProductId = menuProductId;

        Rename(name);
        ChangeGroupType(groupType);
        ChangeSelectionLimits(minSelections, maxSelections);
        ChangeSortOrder(sortOrder);

        Activate();
    }

    public void Rename(string newName)
    {
        Name = Guard.RequiredText(
            newName, 100, nameof(newName));
    }

    public void ChangeGroupType(ChoiceGroupType type)
    {
        if (!Enum.IsDefined(type))
            throw new ArgumentOutOfRangeException(
                nameof(type),
                "Invalid choice group type.");

        GroupType = type;
    }

    public void ChangeSelectionLimits(
        int minSelections,
        int maxSelections)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(
            minSelections,
            nameof(minSelections));

        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(
            maxSelections,
            nameof(maxSelections));

        ArgumentOutOfRangeException.ThrowIfGreaterThan(
            minSelections,
            maxSelections,
            nameof(minSelections));

        MinSelections = minSelections;
        MaxSelections = maxSelections;
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
