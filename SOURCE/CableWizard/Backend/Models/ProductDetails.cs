namespace CableWizardBackend.Models;

public class ProductDetails
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Library { get; set; }
    
    public ProductAttributes Attributes { get; set; }
    public List<string> Connectors { get; set; }
    public List<string> Wires { get; set; }
    public List<string> Pins { get; set; }
}