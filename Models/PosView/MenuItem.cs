using Avalonia.Media.Imaging;
using Avalonia.Platform;
using System;
using System.Collections.Generic;
using System.Text;

namespace CornerPlate.Models.PosView;

public class MenuItem
{
    
    public string Photo { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public double Price { get; set; } = 0.0;
    public bool IsAvailable { get; set; } = true;
    public bool IsCustomizable { get; set; } = false;

    private Bitmap? _image;
    public Bitmap? Image => _image ??= LoadImage();

    private Bitmap? LoadImage()
    {
        if (string.IsNullOrWhiteSpace(Photo)) return null;

        try
        {
            return new Bitmap(AssetLoader.Open(new Uri(Photo)));
        }
        catch
        {
            return null; // Missing or bad file: show nothing instead of crashing the app
        }
    }
}