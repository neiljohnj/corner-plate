
using CornerPlate.Domain.Common;

namespace CornerPlate.Domain.Entities;

public class ProductChoiceOption
{
    public Guid Id { get; private set; }

    public Guid ChoiceGroupId { get; private set; }
    public ProductChoiceGroup ProductChoiceGroup
    { get; private set; } = null!;

    public Guid? ComponentProductId { get; private set; }
    public MenuProduct? ComponentProduct
    { get; private set; }

    public string Label { get; private set; } = null!;
    public decimal PriceAdjustment { get; private set; }
    public int MaxQuantity { get; private set; }
    public bool IsDefault { get; private set; }
    public int SortOrder { get; private set; }
    public bool IsActive { get; private set; }

    private ProductChoiceOption() { }

    public ProductChoiceOption(
        Guid choiceGroupId,
        string label,
        Guid? componentProductId = null)
    {
        if (choiceGroupId == Guid.Empty)
            throw new ArgumentException(
                "Choice group ID is required.",
                nameof(choiceGroupId));

        if (componentProductId == Guid.Empty)
            throw new ArgumentException(
                "Component product ID cannot be empty.",
                nameof(componentProductId));

        Id = Guid.NewGuid();
        ChoiceGroupId = choiceGroupId;
        ComponentProductId = componentProductId;

        Rename(label);
        ChangePriceAdjustment(0m);
        ChangeMaxQuantity(1);
        ChangeSortOrder(0);
        SetAsNotDefault();
        Activate();
    }

    public void Rename(string newLabel)
    {
        Label = Guard.RequiredText(
            newLabel, 120, nameof(newLabel));
    }

    public void ChangePriceAdjustment(decimal priceAdjustment)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(
            priceAdjustment,
            nameof(priceAdjustment));

        if (priceAdjustment > 9999999999.99m)
            throw new ArgumentOutOfRangeException(
                nameof(priceAdjustment),
                "Price adjustment exceeds the allowed amount.");

        if (!Guard.HasAtMostTwoDecimalPlaces(priceAdjustment))
            throw new ArgumentException(
                "Price adjustment cannot exceed two decimal places.",
                nameof(priceAdjustment));

        PriceAdjustment = priceAdjustment;
    }

    public void ChangeMaxQuantity(int maxQuantity)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(
            maxQuantity, nameof(maxQuantity));

        MaxQuantity = maxQuantity;
    }

    public void ChangeSortOrder(int sortOrder)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(
            sortOrder, nameof(sortOrder));

        SortOrder = sortOrder;
    }

    public void SetAsDefault() => IsDefault = true;

    public void SetAsNotDefault() => IsDefault = false;

    public void Activate() => IsActive = true;

    public void Deactivate() => IsActive = false;
}
