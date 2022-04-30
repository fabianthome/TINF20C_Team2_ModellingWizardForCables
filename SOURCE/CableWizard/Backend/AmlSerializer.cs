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
                        
                        // attributes - todo: autmatically choose attribute that doesn't have any child attributes
                        foreach (var attributeClass in unitFamilyType.Attribute)
                        {
                            foreach (var attribute in attributeClass.Attribute)
                            {
                                if (attribute.Name == "Manufacturer")
                                {
                                    productDetails.Manufacturer = attribute.Value;
                                }
                                
                                if (attribute.Name == "ManufacturerURI")
                                {
                                    productDetails.ManufacturerURI = attribute.Value;
                                }
                                
                                if (attribute.Name == "DeviceClass")
                                {
                                    productDetails.DeviceClass = attribute.Value;
                                }
                                
                                if (attribute.Name == "Model")
                                {
                                    productDetails.Model = attribute.Value;
                                }
                                
                                if (attribute.Name == "ProductCode")
                                {
                                    productDetails.ProductCode = attribute.Value;
                                }

                                if (attribute.Name == "IPCode")
                                {
                                    productDetails.IPCode = attribute.Value;
                                }
                                
                                if (attribute.Name == "Material")
                                {
                                    productDetails.Material = attribute.Value;
                                }
                                
                                if (attribute.Name == "Weight")
                                {
                                    productDetails.Weight = attribute.Value;
                                }
                                
                                if (attribute.Name == "Height")
                                {
                                    productDetails.Height = attribute.Value;
                                }
                                
                                if (attribute.Name == "Width")
                                {
                                    productDetails.Width = attribute.Value;
                                }
                                
                                if (attribute.Name == "Length")
                                {
                                    productDetails.Length = attribute.Value;
                                }

                                foreach (var childAttribute in attribute.Attribute)
                                {
                                    if (childAttribute.Name == "TemperatureMin")
                                    {
                                        productDetails.TemperatureMin = childAttribute.Value;
                                    }
                                
                                    if (childAttribute.Name == "TemperatureMax")
                                    {
                                        productDetails.TemperatureMax = childAttribute.Value;
                                    }
                                }
                            }
                        }
                        
                        // connectors
                        productDetails.Connectors = new List<string>();
                        foreach (var externalInterface in unitFamilyType.ExternalInterfaceAndInherited)
                        {
                            //Console.WriteLine($"Connector: {externalInterface}");
                            productDetails.Connectors.Add(externalInterface.ToString());
                        }
                        
                        // wires - doesn't work properly for Balluff lib yet
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
                }
            }
        }
        Console.WriteLine($"No cable with that ID!");
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