using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using CornerPlate.Desktop.Models;

namespace CornerPlate.Desktop.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    public ObservableCollection<NavItem> NavItems { get; } = new()
    {
        new NavItem { Text = "POS", Icon = "fa-solid fa-utensils" },
        new NavItem { Text = "Orders", Icon = "fa-regular fa-file-lines" },
        new NavItem { Text = "Kitchen", Icon = "fa-solid fa-kitchen-set" },
        new NavItem { Text = "Tables", Icon = "fa-solid fa-couch" },
        new NavItem { Text = "More", Icon = "fa-solid fa-ellipsis" }
    };

    public ObservableCollection<OrderTypeItem> OrderTypeItems { get; } = new()
    {
        new OrderTypeItem { Text = "Dine-In", Icon = "fa-solid fa-utensils" },
        new OrderTypeItem { Text = "Takeout", Icon = "fa-solid fa-bag-shopping" },
        new OrderTypeItem { Text = "Drive-Thru", Icon = "fa-solid fa-car" },
        new OrderTypeItem { Text = "Online", Icon = "fa-solid fa-cloud" }
    };

    [ObservableProperty]
    private string _appName = "Corner Plate";

    [ObservableProperty]
    private ViewModelBase _currentPage = new PosViewModel();
}