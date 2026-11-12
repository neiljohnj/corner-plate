using System;

namespace CornerPlate.Desktop.Models.Products;

public class Product
{
    public string Name { get; init; } = "";
    public string Category { get; init; } = "";
    public string Type { get; init; } = "";
    public decimal Price { get; init; } = 0.00m;
    public bool Availability { get; init; } = true;
    public bool Active { get; init; } = true;
}