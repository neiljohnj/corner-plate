using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using CornerPlate.Desktop.Models;
using CornerPlate.Desktop.Models.PosView;
using System;

namespace CornerPlate.Desktop.ViewModels;

public partial class PosViewModel : ViewModelBase
{
    public ObservableCollection<FoodTypeItem> FoodTypeItems { get; } = new()
    {
        new FoodTypeItem { Text = "All Items", Icon = "fa-solid fa-table-cells-large" },
        new FoodTypeItem { Text = "Burgers", Icon = "fa-solid fa-burger" },
        new FoodTypeItem { Text = "Combos", Icon = "fa-solid fa-bowl-food" },
        new FoodTypeItem { Text = "Sides", Icon = "fa-solid fa-drumstick-bite" },
        new FoodTypeItem { Text = "Drinks", Icon = "fa-solid fa-glass-water" }
    };

    public ObservableCollection<MenuItem> MenuItems { get; } = new()
    {
        new MenuItem { Name = "Burger Combo", Price = 224.00, Photo = "avares://CornerPlate.Desktop/Assets/menu-items/burger-combo.png", IsCustomizable = true },
        new MenuItem { Name = "Classic Burger", Price = 168.00, Photo = "avares://CornerPlate.Desktop/Assets/menu-items/classic-burger.webp" },
        new MenuItem { Name = "Chicken Burger", Price = 196.00, Photo = "avares://CornerPlate.Desktop/Assets/menu-items/chicken-burger.png" },
        new MenuItem { Name = "Fries", Price = 56.00, Photo = "avares://CornerPlate.Desktop/Assets/menu-items/fries.png" },
        new MenuItem { Name = "Iced Tea", Price = 56.00, Photo = "avares://CornerPlate.Desktop/Assets/menu-items/iced-tea.png" },
        new MenuItem { Name = "Garden Salad", Price = 112.00, Photo = "avares://CornerPlate.Desktop/Assets/menu-items/garden-salad.webp", IsAvailable = false }
    };

    [ObservableProperty]
    private string _searchText = "";

    partial void OnSearchTextChanged(string value)
    {
        Console.Write(value);
    }
}