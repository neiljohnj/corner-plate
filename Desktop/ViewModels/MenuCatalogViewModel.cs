using CommunityToolkit.Mvvm.ComponentModel;
using CornerPlate.Desktop.Models.Products;
using System.Collections.ObjectModel;

namespace CornerPlate.Desktop.ViewModels;

public partial class MenuCatalogViewModel : ViewModelBase
{
    [ObservableProperty]
    private string? _productSearchText;

    private ObservableCollection<Product> Products { get; } = new()
    {
        new Product { Name = "Burger Combo", Category = "Combos", Type = "Bundle", Price = 224.00m },
        new Product { Name = "Chicken Burger", Category = "Burgers", Type = "Individual", Price = 196.00m },
        new Product { Name = "Chicken Combo", Category = "Combos", Type = "Bundle", Price = 252.00m },
        new Product { Name = "Classic Burger", Category = "Burgers", Type = "Individual", Price = 168.00m },
        new Product { Name = "Fries", Category = "Sides", Type = "Individual", Price = 56.00m },
        new Product { Name = "Garden Salad", Category = "Sides", Type = "Individual", Price = 112.00m, Availability = false },
        new Product { Name = "Iced Tea", Category = "Drinks", Type = "Individual", Price = 56.00m },
        new Product { Name = "Spicy Burger", Category = "Burgers", Type = "Individual", Price = 196.00m, Active = false }
    };
}