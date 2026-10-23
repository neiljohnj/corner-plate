namespace CornerPlate.Domain.Common;

public static class Guard
{
    public static string RequiredText(
        string? value,
        int maxLength,
        string parameterName)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException(
                "Value cannot be empty.",
                parameterName);

        value = value.Trim();

        if (value.Length > maxLength)
            throw new ArgumentOutOfRangeException(
                $"Value cannot exceed {maxLength} characters.",
                parameterName);

        return value;
    }

    public static bool HasAtMostTwoDecimalPlaces(decimal value)
    {
        return decimal.Round(value, 2) == value;
    }
}