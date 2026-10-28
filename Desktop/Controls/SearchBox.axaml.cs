using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;

namespace CornerPlate.Desktop.Controls;

public partial class SearchBox : UserControl
{

    public string? Text
    {
        get => GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    public string Placeholder
    {
        get => GetValue(PlaceholderProperty);
        set => SetValue(PlaceholderProperty, value);
    }

    public static readonly StyledProperty<string?> TextProperty =
        AvaloniaProperty.Register<SearchBox, string?>(
            nameof(Text),
            defaultBindingMode: BindingMode.TwoWay
        );

    public static readonly StyledProperty<string> PlaceholderProperty =
        AvaloniaProperty.Register<SearchBox, string>(
            nameof(Placeholder),
            "Search..."
        );

    public SearchBox()
    {
        InitializeComponent();
    }
}
