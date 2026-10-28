using CornerPlate.Desktop.ViewModels;
using System;

namespace CornerPlate.Desktop.Models;

public class NavItem
{
    public String Text { get; init; } = "";
    public String Icon { get; init; } = "";
    public ViewModelBase Page { get; set; } = null!;

}