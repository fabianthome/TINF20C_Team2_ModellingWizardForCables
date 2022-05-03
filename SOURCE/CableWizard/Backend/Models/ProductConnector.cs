namespace CableWizardBackend.Models;

public class ProductConnector
{
    public string? Type { get; set; }
    public List<ProductPin> Pins { get; set; }
}