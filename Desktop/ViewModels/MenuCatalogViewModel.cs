using CommunityToolkit.Mvvm.ComponentModel;

namespace CornerPlate.Desktop.ViewModels;

public partial class MenuCatalogViewModel : ViewModelBase
{
    [ObservableProperty]
    private string? _productSearchText;
}