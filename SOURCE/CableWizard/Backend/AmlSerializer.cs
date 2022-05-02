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
                        productDetails.Id = id;
                        productDetails.Name = unitFamilyType.Name;
                        productDetails.Library = productLibrary.ToString();
                        var connectorPinIds = new List<string>();
                        var wirePinIds = new List<string>();

                        // attributes
                        productDetails = GetAttributes(productDetails, unitFamilyType);
                        
                        // connectors
                        productDetails = GetConnectors(productDetails, unitFamilyType, ref connectorPinIds);

                        // wires
                        productDetails = GetWires(productDetails, unitFamilyType, ref wirePinIds);

                        // internal links - todo: instead of just logging in terminal, add relations to ProductDetails
                        GetInternalLinks(unitFamilyType, connectorPinIds);

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

    private static ProductDetails GetConnectors(ProductDetails productDetails, SystemUnitFamilyType unitFamilyType, ref List<string> connectorPinIds)
    {
        productDetails.Connectors = new List<string>();
        foreach (var connector in unitFamilyType.ExternalInterfaceAndInherited)
        {
            //Console.WriteLine($"Connector: {externalInterface}");
            productDetails.Connectors.Add(connector.ToString());
            
            foreach (var connectorPin in connector.ExternalInterfaceAndDescendants)
            {
                //Console.WriteLine($"Pin: {connectorPin.Name}, {connectorPin.ID}");
                connectorPinIds.Add(connectorPin.ID);
            }
        }
        
        return productDetails;
    }
    
    private static ProductDetails GetWires(ProductDetails productDetails, SystemUnitFamilyType unitFamilyType, ref List<string> wirePinIds)
    {
        productDetails.Wires = new List<string>();
        productDetails.Pins = new List<string>();
        foreach (var wireClass in unitFamilyType.InternalElementAndInherited)
        {
            var wiresList = DeepSearchWires(wireClass);

            foreach (var wire in wiresList)
            {
                foreach (var roleReq in wire.RoleRequirements)
                {
                    if (roleReq.RefBaseRoleClassPath == "CableRCL/Wire")
                    {
                        //Console.WriteLine($"added {wire}");
                        productDetails.Wires.Add(wire.ToString());
                    }
                }
                
                // pins
                foreach (var pin in wire.ExternalInterface)
                {
                    if (pin.RefBaseClassPath == "AutomationMLComponentBaseICL/ElectricInterface")
                    {
                        //Console.WriteLine($"Pin: {pin}");
                        productDetails.Pins.Add(pin.ToString());
                        wirePinIds.Add(pin.ID);
                    }
                }
                
                /*
                // access colours of wires (c1 etc.)
                foreach (var attribute in wire.Attribute)
                {
                    Console.WriteLine($"{attribute.Value}");
                }
                */
            }
        }

        return productDetails;
    }

    private static List<InternalElementType> DeepSearchWires(InternalElementType wire)
    {
        if (wire.InternalElement.Count == 0)
        {
            return new List<InternalElementType> {wire};
        }
        
        var wires = new List<InternalElementType>();

        foreach (var childWire in wire.InternalElement)
        {
            wires = wires.Concat(DeepSearchWires(childWire)).ToList();
        }
        
        return wires;
    }

    private static void GetInternalLinks(SystemUnitFamilyType unitFamilyType, List<string> connectorPinIds)
    {
        foreach (var internalLink in unitFamilyType.InternalLink)
        {
            Console.WriteLine($"{internalLink.Name}: {internalLink.RefPartnerSideA} & {internalLink.RefPartnerSideB}");

            foreach (var connectorPinId in connectorPinIds)
            {
                if (connectorPinId == internalLink.RefPartnerSideA)
                {
                    var connectorPin =
                        Document.CAEXFile.FindCaexObjectFromId<ExternalInterfaceType>(internalLink
                            .RefPartnerSideA);
                    var wirePin =
                        Document.CAEXFile.FindCaexObjectFromId<ExternalInterfaceType>(internalLink
                            .RefPartnerSideB);

                    Console.WriteLine($"{connectorPin.CAEXParent}: {connectorPin} & {wirePin.CAEXParent}");
                }
                else if (connectorPinId == internalLink.RefPartnerSideB)
                {
                    var connectorPin =
                        Document.CAEXFile.FindCaexObjectFromId<ExternalInterfaceType>(internalLink
                            .RefPartnerSideB);
                    var wirePin =
                        Document.CAEXFile.FindCaexObjectFromId<ExternalInterfaceType>(internalLink
                            .RefPartnerSideA);

                    Console.WriteLine($"{connectorPin.CAEXParent}: {connectorPin} & {wirePin.CAEXParent}");
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