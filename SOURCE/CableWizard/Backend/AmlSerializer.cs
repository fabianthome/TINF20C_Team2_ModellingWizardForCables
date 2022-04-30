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
                    if (unitFamilyType.ID == id)
                    {
                        productDetails.Id = id;
                        productDetails.Name = unitFamilyType.Name;
                        productDetails.Library = productLibrary.ToString();
                        productDetails.Connectors = new List<string>();
                        productDetails.Wires = new List<string>();
                        productDetails.Pins = new List<string>();

                        // cables
                        Console.WriteLine($"Cable: {unitFamilyType}");
                        
                        // connectors
                        foreach (var externalInterface in unitFamilyType.ExternalInterfaceAndInherited)
                        {
                            Console.WriteLine($"Connector: {externalInterface}");
                            productDetails.Connectors.Add(externalInterface.ToString());
                        }
                        
                        // pins - doesn't work properly for Balluff lib yet
                        foreach (var wire in unitFamilyType.InternalElementAndInherited)
                        {
                            // todo: how to filter internalElement for those that have a <RoleRequirements RefBaseRoleClassPath="CableRCL/Wire" /> child?
                        
                            /*
                            // access colours of wires (c1 etc.)
                            foreach (var attribute in wire.Attribute)
                            {
                                Console.WriteLine($"{attribute.Value}");
                            }
                            */
                        
                            Console.WriteLine($"Wire: {wire}");
                            productDetails.Wires.Add(wire.ToString());

                            foreach (var pin in wire.ExternalInterface)
                            {
                                Console.WriteLine($"Pin: {pin}");
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