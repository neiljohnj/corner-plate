using CommunityToolkit.Mvvm.ComponentModel;
using CornerPlate.Desktop.Models.Enums;
using CornerPlate.Desktop.Services.Interfaces;
using CornerPlate.Desktop.ViewModels;
using System;

namespace CornerPlate.Desktop.Services;

public partial class NavigationService : ObservableObject, INavigationService
{
    [ObservableProperty]
    private PageId? currentPageId;

    [ObservableProperty]
    private ViewModelBase? currentPage;

    public void NavigateTo(PageId destination)
    {
        // Do nothing if the destination is the same page
        if (CurrentPageId == destination)
            return;

        ViewModelBase nextPage = destination switch
        {
            PageId.Pos => new PosViewModel(),
            PageId.Orders => new OrdersViewModel(),
            PageId.Menu => new MenuCatalogViewModel(),
            _ => throw new NotSupportedException(
                $"Page '{destination}' is not implemented yet")
        };

        CurrentPage = nextPage;
        CurrentPageId = destination;
    }
}