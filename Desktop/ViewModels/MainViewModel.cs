using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using CornerPlate.Desktop.Models;
using CornerPlate.Desktop.Models.Enums;
using CornerPlate.Desktop.Services.Interfaces;
using CornerPlate.Desktop.Services;
using System.Linq;

namespace CornerPlate.Desktop.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    // Attaching Navigation Service
    public INavigationService Navigation { get; } = new NavigationService();

    public ObservableCollection<NavItem> NavItems { get; } = new()
    {
        new NavItem
        {
            Id = PageId.Pos,
            Text = "POS",
            Icon = "fa-solid fa-utensils"
        },
        new NavItem
        {
            Id = PageId.Orders,
            Text = "Orders",
            Icon = "fa-regular fa-file-lines",
        },
        new NavItem
        {
            Id = PageId.Menu,
            Text = "Menu",
            Icon = "fa-solid fa-book-open"
        }
        //new NavItem { Text = "POS", Icon = "fa-solid fa-utensils" },
        //new NavItem { Text = "Orders", Icon = "fa-regular fa-file-lines" },
        //new NavItem { Text = "Kitchen", Icon = "fa-solid fa-kitchen-set" },
        //new NavItem { Text = "Tables", Icon = "fa-solid fa-couch" },
        //new NavItem { Text = "More", Icon = "fa-solid fa-ellipsis" }
    };

    public ObservableCollection<OrderTypeItem> OrderTypeItems { get; } = new()
    {
        new OrderTypeItem { Text = "Dine-In", Icon = "fa-solid fa-utensils" },
        new OrderTypeItem { Text = "Takeout", Icon = "fa-solid fa-bag-shopping" },
        new OrderTypeItem { Text = "Drive-Thru", Icon = "fa-solid fa-car" },
        new OrderTypeItem { Text = "Online", Icon = "fa-solid fa-cloud" }
    };

    // Observable Properties

    [ObservableProperty]
    private string _appName = "Corner Plate";

    [ObservableProperty]
    private NavItem? _selectedNavItem;
    
    // Constructor
    public MainViewModel()
    {
        Navigation.PropertyChanged += (_, e) =>
        {
            if (e.PropertyName ==
                    nameof(INavigationService.CurrentPageId))
            {
                SelectedNavItem = NavItems.FirstOrDefault(
                    item => item.Id == Navigation.CurrentPageId);
            }
        };

        // Initial Navigation
        SelectedNavItem = NavItems[0];
    }

    partial void OnSelectedNavItemChanged(NavItem? value)
    {
        if (value is not null)
            Navigation.NavigateTo(value.Id);
    }
}