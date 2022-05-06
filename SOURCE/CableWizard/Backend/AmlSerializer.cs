using System.Text;
using Aml.Engine.AmlObjects;
using Aml.Engine.CAEX;
using Aml.Engine.CAEX.Extensions;
using CableWizardBackend.Models;
using Newtonsoft.Json;

namespace CableWizardBackend;

public static class AmlSerializer
{
    private static readonly CAEXDocument document;

    private static string AmlName = "Template.aml";

    static AmlSerializer()
    {
        /*var filepath = $"{Directory.GetCurrentDirectory()}/Workdir/Cables_28032022.amlx";
        //todo get this path over environment variables
        var container = new AutomationMLContainer(filepath);
        Document = CAEXDocument.LoadFromStream(container.RootDocumentStream());*/
        
        document = CAEXDocument.LoadFromFile("Workdir/" + AmlName);
    }

    public static IEnumerable<string> GetProducts()
    {
        var systemUnitClassLib = document.CAEXFile.SystemUnitClassLib;

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
        var systemUnitClassLib = document.CAEXFile.SystemUnitClassLib;

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
                        var wirePinIds = new List<string>();

                        // attributes
                        productDetails = GetAttributes(productDetails, unitFamilyType);
                        
                        // connectors
                        productDetails = GetConnectors(productDetails, unitFamilyType);

                        // wires
                        productDetails = GetWires(productDetails, unitFamilyType, ref wirePinIds);

                        return productDetails;
                    }
                }
            }
        }
        Console.WriteLine($"No cable with that ID!");
        return productDetails;
    }

    public static bool DeleteProduct(string id)
    {
        var cable = document.CAEXFile.FindCaexObjectFromId<SystemUnitFamilyType>(id);
        if (cable != null)
        {
            // only cables can be deleted
            foreach (var roleClass in cable.SupportedRoleClass)
            {
                if (roleClass.RefRoleClassPath == "CableRCL/Cable")
                {
                    // delete cable
                    cable.Remove(removeRelations: true);
                    document.SaveToFile("Workdir/" + AmlName, true);
                    return true;
                }
            }
        }

        return false;
    }
    
    public static void CreateProduct(ProductDetails productDetails)
    {
        // add library if not already existing
        SystemUnitClassLibType productLib = null;
        foreach (var lib in document.CAEXFile.SystemUnitClassLib)
        {
            if (lib.Name == productDetails.Library)
            {
                productLib = lib;
                break;
            }
        }
        if (productLib == null)
        {
            productLib = document.CAEXFile.SystemUnitClassLib.Append("ProductLibrary_" + productDetails.Library);
        }

        // add cable if not already existing (if existing, delete and add new)
        SystemUnitClassType cable = null;
        foreach (var cab in productLib.SystemUnitClass)
        {
            if (cab.Name == productDetails.Name)
            {
                DeleteProduct(cab.ID);
                break;
            }
        }

        if (cable == null)
        {
            cable = productLib.SystemUnitClass.Append(productDetails.Name);
        }

        // add attributes
        var data = cable.Attribute.Append("Data");
        AddAttributes(productDetails, data);

        // add connectors & pins
        var numberConnectors = 0;
        var pinIdsList = new List<List<Tuple<string, string>>>();
        foreach (var connectorInfo in productDetails.Connectors)
        {
            var pinIds = new List<Tuple<string, string>>(); // list containing tuples like ("11b49049-95fe-42bb-9c16-f275e4995acd", "C1P1")
            numberConnectors++;
            var connector = cable.ExternalInterface.Append(connectorInfo.Type);
            foreach (var pinInfo in connectorInfo.Pins)
            {
                var pin = connector.ExternalInterface.Append(pinInfo.Name);
                pinIds.Add(new Tuple<string, string>(pin.ID, pinInfo.ConnectedWire + "P" + numberConnectors)); // used later on for adding links
            }
            pinIdsList.Add(pinIds);
        }

        // add role class
        var roleClass = cable.SupportedRoleClass.Append();
        roleClass.RefRoleClassPath = "CableRCL/Cable";
        
        // add wiring
        var wireDir = cable.InternalElement.Append("Wiring");
        var wirePinIdsList = new List<List<Tuple<string, string>>>();
        foreach (var wireInfo in productDetails.Wires)
        {
            var wirePinIds = new List<Tuple<string, string>>(); // list containing tuples like ("31ecc4c5-490a-4e2d-ba6a-0b5210d648be", "C1P1")
            var wire = wireDir.InternalElement.Append(wireInfo);
            for (var i = 1; i <= numberConnectors; i++) // i serves as help for naming pins
            {
                var wirePin = wire.ExternalInterface.Append("P" + i);
                wirePinIds.Add(new Tuple<string, string>(wirePin.ID, wire.Name + wirePin.Name)); // used later on for adding links
            }
            wirePinIdsList.Add(wirePinIds);
        }

        // add links
        var numberLinks = 0; // serves as help for naming links
        foreach (var pinList in pinIdsList)
        {
            foreach (var pin in pinList)
            {
                foreach (var wirePinList in wirePinIdsList)
                {
                    foreach (var wirePin in wirePinList)
                    {
                        if (pin.Item2 == wirePin.Item2) // check if pin and wire pin belong together
                        {
                            //Console.WriteLine($"{pinId.Item1} & {wirePinId.Item1}"); // test
                            var link = cable.InternalLink.Append("InternalLink" + numberLinks);
                            link.RefPartnerSideA = pin.Item1; // add pin id as partner a
                            link.RefPartnerSideB = wirePin.Item1; // add wire pin id as partner b
                            numberLinks++;
                        }
                    }
                }
            }
        }

        // save aml file
        document.SaveToFile("Workdir/" + AmlName, true);
    }

    public static void AddAttributes(ProductDetails productDetails, AttributeType data)
    {
        var manufacturer = data.Attribute.Append("Manufacturer");
        manufacturer.AttributeDataType = "xs:string";
        manufacturer.Description =
            "Name of the Manufacturer (person, company or organisation)";
        manufacturer.Value = productDetails.Attributes.Manufacturer;
        
        var manufacturerUri = data.Attribute.Append(("ManufacturerURI"));
        manufacturerUri.AttributeDataType = "xs:string";
        manufacturerUri.Description = "Address of the product manufacturer on the world wide web (URL)";
        manufacturerUri.Value = productDetails.Attributes.ManufacturerUri;
        
        var deviceClass = data.Attribute.Append("DeviceClass");
        deviceClass.AttributeDataType = "xs:string";
        deviceClass.Description =
            "Product family name of the manufacturer, characterization may be based on its usage, operation principle, and its fabricated form";
        deviceClass.Value = productDetails.Attributes.DeviceClass;
        
        var model = data.Attribute.Append("Model");
        model.AttributeDataType = "xs:string";
        model.Description = "Product name or model code of the manufacturer";
        model.Value = productDetails.Attributes.Model;
        
        var productCode = data.Attribute.Append("ProductCode");
        productCode.AttributeDataType = "xs:string";
        productCode.Description = "Unique product identifier given by the manufacturer";
        productCode.Value = productDetails.Attributes.ProductCode;
        
        var temperatureMin = data.Attribute.Append("TemperatureMin");
        temperatureMin.AttributeDataType = "xs:int";
        temperatureMin.Unit = "°C";
        temperatureMin.Description =
            "Lowest ambient temperature for which the component operates within its specified limits.";
        temperatureMin.Value = productDetails.Attributes.TemperatureMin.ToString();
        
        var temperatureMax = data.Attribute.Append("TemperatureMax");
        temperatureMax.AttributeDataType = "xs:int";
        temperatureMax.Unit = "°C";
        temperatureMax.Description =
            "Highest ambient temperature for which the component operates within its specified limits.";
        temperatureMax.Value = productDetails.Attributes.TemperatureMax.ToString();
        
        var ipCode = data.Attribute.Append("IPCode");
        ipCode.AttributeDataType = "xs:string";
        ipCode.Description =
            "Degree of protection (IP code) of the component provided by enclosure, numerical classification in accordance with IEC 60529 preceded by the symbol IP.";
        ipCode.Value = productDetails.Attributes.IpCode;
        
        var material = data.Attribute.Append("Material");
        material.AttributeDataType = "xs:string";
        material.Description = "Basic material of the housing of the component.";
        material.Value = productDetails.Attributes.Material;
        
        var weight = data.Attribute.Append("Weight");
        weight.AttributeDataType = "xs:float";
        weight.Unit = "g";
        weight.Description =
            "Net weight: Value of the mass of the component with all fixed parts without packaging and accessories.";
        weight.Value = productDetails.Attributes.Weight.ToString();
        
        var height = data.Attribute.Append("Height");
        height.AttributeDataType = "xs:int";
        height.Unit = "mm";
        height.Description =
            "Height of the body, vertical distance between the top and bottom of the component when standing in its normal position of use, including connectors and terminals, without accessory and cable.";
        height.Value = productDetails.Attributes.Height.ToString();
        
        var width = data.Attribute.Append("Width");
        width.AttributeDataType = "xs:int";
        width.Unit = "mm";
        width.Description =
            "Width or breadth of the body, horizontal distance between the left-hand and right-hand extremes of the component when standing in its normal position of use, including connectors and terminals, without accessory and cable.";
        width.Value = productDetails.Attributes.Width.ToString();
        
        var length = data.Attribute.Append("Length");
        length.AttributeDataType = "xs:int";
        length.Unit = "mm";
        length.Description =
            "Length of the body, horizontal distance between the front and back of the component when standing in its normal position of use, including connectors and terminals, without accessory and cable.";
        length.Value = productDetails.Attributes.Length.ToString();
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
        // add connectors
        productDetails.Connectors = new List<ProductConnector>();
        
        foreach (var connector in unitFamilyType.ExternalInterfaceAndInherited)
        {
            var productConnector = new ProductConnector()
            {
                Type = connector.Name
            };

            // add pins to connectors
            productConnector.Pins = new List<ProductPin>();
            
            foreach (var connectorPin in connector.ExternalInterfaceAndDescendants)
            {
                var productPin = new ProductPin()
                {
                    Name = connectorPin.Name
                };

                // add connected wires to pins
                GetLinks(unitFamilyType, connectorPin.ID, productPin);
                
                productConnector.Pins.Add(productPin);
            }
            
            productDetails.Connectors.Add(productConnector);
        }
        
        return productDetails;
    }
    
    private static ProductDetails GetWires(ProductDetails productDetails, SystemUnitFamilyType unitFamilyType, ref List<string> wirePinIds)
    {
        productDetails.Wires = new List<string>();
        //productDetails.Pins = new List<string>(); //not needed?!
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
                
                // add pin ids to list to access connected wires later on
                foreach (var pin in wire.ExternalInterface)
                {
                    if (pin.RefBaseClassPath == "AutomationMLComponentBaseICL/ElectricInterface")
                    {
                        wirePinIds.Add(pin.ID);
                        //productDetails.Pins.Add(pin.ToString()); //not needed?!
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
    
    private static void GetLinks(SystemUnitFamilyType unitFamilyType, string connectorPinId, ProductPin productPin)
    {
        foreach (var internalLink in unitFamilyType.InternalLink)
        {
            //Console.WriteLine($"{internalLink.Name}: {internalLink.RefPartnerSideA} & {internalLink.RefPartnerSideB}");

            // if pin id is saved in side A of internal link
            if (connectorPinId == internalLink.RefPartnerSideA)
            {
                var connectorPin =
                    document.CAEXFile.FindCaexObjectFromId<ExternalInterfaceType>(internalLink
                        .RefPartnerSideA);
                var wirePin =
                    document.CAEXFile.FindCaexObjectFromId<ExternalInterfaceType>(internalLink
                        .RefPartnerSideB);
                
                //Console.WriteLine($"{connectorPin.CAEXParent}: {connectorPin} & {wirePin.CAEXParent}");
                productPin.ConnectedWire = wirePin.CAEXParent.ToString();
            }
            // if pin id is saved in side B of internal link
            else if (connectorPinId == internalLink.RefPartnerSideB)
            {
                var connectorPin =
                    document.CAEXFile.FindCaexObjectFromId<ExternalInterfaceType>(internalLink
                        .RefPartnerSideB);
                var wirePin =
                    document.CAEXFile.FindCaexObjectFromId<ExternalInterfaceType>(internalLink
                        .RefPartnerSideA);

                //Console.WriteLine($"{connectorPin.CAEXParent}: {connectorPin} & {wirePin.CAEXParent}");
                productPin.ConnectedWire = wirePin.CAEXParent.ToString();
            }
        }
    }
    
    private static List<SystemUnitFamilyType> DeepSearch(SystemUnitFamilyType familyType)
    {
        if (familyType.SystemUnitClass.Count == 0)
        {
            // only add to list, if it's really a cable
            foreach (var roleClass in familyType.SupportedRoleClass)
            {
                if (roleClass.RefRoleClassPath == "CableRCL/Cable")
                {
                    return new List<SystemUnitFamilyType> {familyType};
                }
            }
        }
    
        List<SystemUnitFamilyType> results = new List<SystemUnitFamilyType>();
        
        foreach (var inner in familyType.SystemUnitClass)
        {
            results = results.Concat(DeepSearch(inner)).ToList();
        }
        
        return results;
    }
}