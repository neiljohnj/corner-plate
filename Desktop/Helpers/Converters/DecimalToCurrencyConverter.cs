using Avalonia.Data.Converters;
using System;
using System.Globalization;

namespace CornerPlate.Desktop.Helpers.Converters;

public class DecimalToCurrencyConverter : IValueConverter
{
    public object? Convert(
        object? value, 
        Type targetType, 
        object? parameter, 
        CultureInfo culture)
    {
        if (value is decimal amount)
        {
            return amount.ToString("C", culture);
        }

        return "0.00";
    }

    public object? ConvertBack(
        object? value,
        Type targetType,
        object? parameter,
        CultureInfo culture)
    {
        if (value is string text &&
            decimal.TryParse(text, NumberStyles.Currency, culture, out decimal result))
        {
            return result;
        }

        return 0.00m;
    }
}