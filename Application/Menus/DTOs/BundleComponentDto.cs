namespace CornerPlate.Application.Menus.DTOs;

public sealed record AddBundleComponentRequest(
    Guid BundleProductId,
    Guid ComponentProductId,
    int Quantity = 1,
    int SortOrder = 0);

public sealed record BundleComponentDTO(
    Guid BundleProductid,
    Guid ComponentProductId,
    string ComponentProductName,
    int Quantity,
    int SortOrder);