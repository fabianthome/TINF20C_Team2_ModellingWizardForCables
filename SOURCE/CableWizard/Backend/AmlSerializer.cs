using Aml.Engine.AmlObjects;
using Aml.Engine.CAEX;
using Aml.Engine.CAEX.Extensions;
using CableWizardBackend.Models;

namespace CableWizardBackend;

public static class AmlSerializer
{
    private static readonly CAEXDocument Document;

    static AmlSerializer()
    {
        var filepath = $"{Directory.GetCurrentDirectory()}/Workdir/Cables_28032022.amlx";
        //todo get this path over environment variables
        var container = new AutomationMLContainer(filepath);
        Document = CAEXDocument.LoadFromStream(container.RootDocumentStream());
    }

    public static IEnumerable<string> GetProducts()
    {
        var systemUnitClassLib = Document.CAEXFile.SystemUnitClassLib;

        var productList = new List<string>();

        foreach (var productLibrary in systemUnitClassLib)
        {
            foreach (var systemUnitFamilyType in productLibrary.SystemUnitClass)
            {
                var list = DeepSearch(systemUnitFamilyType);
                
                foreach (var unitFamilyType in list)
                {
                    productList.Add(unitFamilyType.ID);
                }
            }
        }
        return productList;
    }

    public static ProductDetails GetProductDetails(string id)
    {
        var systemUnitClassLib = Document.CAEXFile.SystemUnitClassLib;

        var productDetails = new ProductDetails();

        foreach (var productLibrary in systemUnitClassLib)
        {
            foreach (var systemUnitFamilyType in productLibrary.SystemUnitClass)
            {
                var list = DeepSearch(systemUnitFamilyType);

                foreach (var unitFamilyType in list)
                {
                    if (unitFamilyType.ID == id) // search for requested id
                    {
                        // cables
                        //Console.WriteLine($"Cable: {unitFamilyType}");
                        productDetails.Id = id;
                        productDetails.Name = unitFamilyType.Name;
                        productDetails.Library = productLibrary.ToString();
                        
                        // attributes
                        productDetails = GetAttributes(productDetails, unitFamilyType);
                        
                        // connectors
                        productDetails = GetConnectors(productDetails, unitFamilyType);
                        
                        // wires - todo: doesn't work properly for Balluff lib yet (DeepSearchWires() needed)
                        productDetails = GetWires(productDetails, unitFamilyType);
                        
                        return productDetails;
                    }
                }
            }
        }
        Console.WriteLine($"No cable with that ID!");
        return productDetails;
    }

    private static ProductDetails GetAttributes(ProductDetails productDetails, SystemUnitFamilyType unitFamilyType)
    {
        var attributes = new ProductAttributes();
        
        foreach (var attributeClass in unitFamilyType.Attribute)
        {
            var attributesList = DeepSearchAttributes(attributeClass);
            
            foreach (var attribute in attributesList)
            {
                switch (attribute.Name)
                {
                    case "Manufacturer": 
                        attributes.Manufacturer = attribute.Value;
                        break;
                    case "ManufacturerURI":
                        attributes.ManufacturerUri = attribute.Value;
                        break;
                    case "DeviceClass":
                        attributes.DeviceClass = attribute.Value;
                        break;
                    case "Model":
                        attributes.Model = attribute.Value;
                        break;
                    case "ProductCode":
                        attributes.ProductCode = attribute.Value;
                        break;
                    case "TemperatureMin":
                        attributes.TemperatureMin = double.Parse(attribute.Value);
                        break;
                    case "TemperatureMax":
                        attributes.TemperatureMax = double.Parse(attribute.Value);
                        break;
                    case "IPCode":
                        attributes.IpCode = attribute.Value;
                        break;
                    case "Material":
                        attributes.Material = attribute.Value;
                        break;
                    case "Weight":
                        attributes.Weight = double.Parse(attribute.Value);
                        break;
                    case "Height":
                        attributes.Height = double.Parse(attribute.Value);
                        break;
                    case "Width":
                        attributes.Width = double.Parse(attribute.Value);
                        break;
                    case "Length":
                        attributes.Length = double.Parse(attribute.Value);
                        break;
                }
            }
        }

        productDetails.Attributes = attributes;
        return productDetails;
    }
    
    private static List<AttributeType> DeepSearchAttributes(AttributeType attribute)
    {
        if (attribute.Attribute.Count == 0)
        {
            return new List<AttributeType> {attribute};
        }
        
        var attributes = new List<AttributeType>();

        foreach (var childAttribute in attribute.Attribute)
        {
            attributes = attributes.Concat(DeepSearchAttributes(childAttribute)).ToList();
        }

        return attributes;
    }

    private static ProductDetails GetConnectors(ProductDetails productDetails, SystemUnitFamilyType unitFamilyType)
    {
        productDetails.Connectors = new List<string>();
        foreach (var externalInterface in unitFamilyType.ExternalInterfaceAndInherited)
        {
            //Console.WriteLine($"Connector: {externalInterface}");
            productDetails.Connectors.Add(externalInterface.ToString());
        }
        
        return productDetails;
    }

    private static ProductDetails GetWires(ProductDetails productDetails, SystemUnitFamilyType unitFamilyType)
    {
        productDetails.Wires = new List<string>();
        productDetails.Pins = new List<string>();
        foreach (var wire in unitFamilyType.InternalElementAndInherited)
        {
            // todo: how to filter wire for those that have a <RoleRequirements RefBaseRoleClassPath="CableRCL/Wire" /> child?
                        
            //Console.WriteLine($"Wire: {wire}");
            productDetails.Wires.Add(wire.ToString());
                            
            /*
            // access colours of wires (c1 etc.)
            foreach (var attribute in wire.Attribute)
            {
                Console.WriteLine($"{attribute.Value}");
            }
            */

            // pins - todo: same problem as wires
            foreach (var pin in wire.ExternalInterface)
            {
                //Console.WriteLine($"Pin: {pin}");
                productDetails.Pins.Add(pin.ToString());
            }
        }

        return productDetails;
    }
    
    private static List<SystemUnitFamilyType> DeepSearch(SystemUnitFamilyType familyType)
    {
        if (familyType.SystemUnitClass.Count == 0)
        {
            return new List<SystemUnitFamilyType> {familyType};
        }
    
        List<SystemUnitFamilyType> results = new List<SystemUnitFamilyType>();
        
        foreach (var inner in familyType.SystemUnitClass)
        {
            results = results.Concat(DeepSearch(inner)).ToList();
        }
        
        return results;
    }
}