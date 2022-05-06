namespace CableWizardBackend.Models;

public class ProductDetails
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Library { get; set; }
    
    public ProductAttributes Attributes { get; set; }
    public List<string> Wires { get; set; }
    public List<ProductConnector> Connectors { get; set; }
}