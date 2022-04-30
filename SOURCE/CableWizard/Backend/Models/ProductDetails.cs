namespace CableWizardBackend.Models;

public class ProductDetails
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Library { get; set; }
    public List<string> Connectors { get; set; }
    public List<string> Wires { get; set; }
    public List<string> Pins { get; set; }
    public string Manufacturer { get; set; }
    public string ManufacturerURI { get; set; }
    public string DeviceClass { get; set; }
    public string Model { get; set; }
    public string ProductCode { get; set; }
    public string TemperatureMin { get; set; }
    public string TemperatureMax { get; set; }
    public string IPCode { get; set; }
    public string Material { get; set; }
    public string Weight { get; set; }
    public string Height { get; set; }
    public string Width { get; set; }
    public string Length { get; set; }
}