using Aml.Engine.AmlObjects;
using Aml.Engine.CAEX;
using Aml.Engine.CAEX.Extensions;
using System.Text.Json;

namespace CableWizardBackend;

public static class AmlSerializer
{
    private class Product
    {
        public string Name { get; set; }
        public string Id { get; set; }
    }
    
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

    public static void GetProductDetails(string id) // WIP
    {
        Console.WriteLine("looking for product...");
        var systemUnitClassLib = Document.CAEXFile.SystemUnitClassLib;

        var productList = new List<Product>();

        foreach (var productLibrary in systemUnitClassLib)
        {
            //product lib
            //Console.WriteLine($"Product library: {productLibrary}");
            
            foreach (var systemUnitFamilyType in productLibrary.SystemUnitClass)
            {
                var list = DeepSearch(systemUnitFamilyType);

                foreach (var unitFamilyType in list)
                {
                    if (unitFamilyType.ID == id)
                    {
                        //Product product = new Product
                        //{
                        //    Name = unitFamilyType.Name,
                        //    Id = unitFamilyType.ID
                        //};
                        
                        // cables
                        Console.WriteLine($"Cable: {unitFamilyType}");
                        
                        // connectors
                        foreach (var externalInterface in unitFamilyType.ExternalInterfaceAndInherited)
                        {
                            Console.WriteLine($"Connector: {externalInterface}");
                        }
                        
                        // pins - doesn't work properly for Balluff lib yet
                        foreach (var internalElement in unitFamilyType.InternalElementAndInherited)
                        {
                            // todo: how to filter internalElement for those that have a <RoleRequirements RefBaseRoleClassPath="CableRCL/Wire" /> child?
                        
                            // access colours of wires (c1 etc.)
                            foreach (var attribute in internalElement.Attribute)
                            {
                                //Console.WriteLine($"{attribute.Value}");
                            }
                        
                            Console.WriteLine($"Pin: {internalElement}");
                        }
                    }
                }
            }
        }
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