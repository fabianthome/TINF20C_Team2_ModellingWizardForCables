namespace CableWizardBackend.Models;

public class ProductAttributes
{
    public string? Manufacturer { get; set; }
    public string? ManufacturerUri { get; set; }
    public string? DeviceClass { get; set; }
    public string? Model { get; set; }
    public string? ProductCode { get; set; }
    public string? IpCode { get; set; }
    public string? Material { get; set; }
    public double? Length { get; set; }
    public double? Width { get; set; }
    public double? Height { get; set; }
    public double? Weight { get; set; }
    public double? TemperatureMin { get; set; }
    public double? TemperatureMax { get; set; }
}