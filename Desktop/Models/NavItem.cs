using CornerPlate.Desktop.Models.Enums;
using CornerPlate.Desktop.ViewModels;
using System;

namespace CornerPlate.Desktop.Models;

public class NavItem
{
    public required PageId Id { get; init; }
    public String Text { get; init; } = "";
    public String Icon { get; init; } = "";
}