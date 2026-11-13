using Avalonia.Data.Converters;
using System;
using System.Globalization;

namespace CornerPlate.Desktop.Helpers.Converters;

public class AvailabilityStatusConverter : IValueConverter
{
    public object? Convert(
        object? value,
        Type targetType,
        object? parameter,
        CultureInfo culture)
    {
        if (value is bool available)
        {
            return available ? "Available" : "Not Available";
        }

        return "Unknown";
    }

    public object? ConvertBack(
        object? value,
        Type targetType,
        object? parameter,
        CultureInfo culture)
    {
        if (value is string text &&
            bool.TryParse(text, out bool result))
        {
            return result;
        }

        throw new InvalidCastException("Availability status not valid.");
    }
}