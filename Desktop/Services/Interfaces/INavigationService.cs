using System.ComponentModel;
using CornerPlate.Desktop.Models;
using CornerPlate.Desktop.Models.Enums;
using CornerPlate.Desktop.ViewModels;

namespace CornerPlate.Desktop.Services.Interfaces;

public interface INavigationService : INotifyPropertyChanged
{
    PageId? CurrentPageId { get; }
    ViewModelBase? CurrentPage { get; }
    void NavigateTo(PageId destination);
}