using Avalonia.Data.Converters;
using System;
using System.Globalization;

namespace CornerPlate.Desktop.Helpers.Converters;

public class ActiveStatusConverter : IValueConverter
{
    public object? Convert(
        object? value,
        Type targetType,
        object? parameter,
        CultureInfo culture)
    {
        if (value is bool active)
        {
            return active ? "Active" : "Inactive";
        }

        return "Unknown";
    }

    public object ConvertBack(
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

        throw new InvalidCastException("Active status not valid.");
    }
}