using System.Text;
using Aml.Engine.Adapter;
using Aml.Engine.AmlObjects;
using Aml.Engine.CAEX;
using Aml.Engine.CAEX.Extensions;
using Aml.Engine.Services;
using CableWizardBackend.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Newtonsoft.Json;

namespace CableWizardBackend;

public static class AmlSerializer
{
    private static readonly CAEXDocument document;

    private const string workdir = "data/";
    
    private static string AmlName = "Cables3_0.aml";

    static AmlSerializer()
    {
        document = CAEXDocument.LoadFromFile(workdir + AmlName);
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

    public static List<Tuple<string, string>> GetPossibleConnectors()
    {
        var possibleConnectors = new List<Tuple<string, string>>();
        var interfaceClassLib = document.CAEXFile.InterfaceClassLib;

        foreach (var interfaceClass in interfaceClassLib)
        {
            foreach (var interfaceFamilyType in interfaceClass.InterfaceClass)
            {
                var list = DeepSearchInterfaceClass(interfaceFamilyType);
                
                foreach (var possibleConnector in list)
                { 
                    //Console.WriteLine($"{possibleConnector.Name}: \"{possibleConnector.RefBaseClassPath}\"");
                    possibleConnectors.Add(new Tuple<string, string>(possibleConnector.Name, possibleConnector.RefBaseClassPath));
                }
            }
        }

        return possibleConnectors;
    }

    public static string ConvertFile()
    {
        var AmlName215 = "Cables2_15.aml";
        
        // register the transformation service. After registration of the service, the AMLEngine
        // communicates with the transformation service via event notification.
        var transformer = CAEXSchemaTransformer.Register();
        CAEXSchemaTransformer.Register();

        // transform the document to AutomationML 2.10 and CAEX 3.0
        var doc215 = transformer.TransformTo(document, CAEXDocument.CAEXSchema.CAEX2_15);

        // unregister the transformation service. The communication channel between the AMLEngine and
        // the transformation service is closed.
        CAEXSchemaTransformer.UnRegister();

        doc215.SaveToFile(workdir + AmlName215, true);
        
        // validate the document according to the assigned CAEX version
        if (doc215.Validate(out var message))
        {
            Console.WriteLine("Transformation success!");
            
        }
        else
        {
            Console.WriteLine("Transformation failed! See the returned message for details.");
        }

        return AmlName215;
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
                    document.SaveToFile(workdir + AmlName, true);
                    return true;
                }
            }
        }

        return false;
    }
    
    public static string CreateProduct(ProductDetails productDetails)
    {
        var caexVersion = "2.15";
        var status = "Product created.";
    
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
            productLib = document.CAEXFile.SystemUnitClassLib.Append(productDetails.Library);
        }

        // add cable if not already existing (if existing, delete and add new)
        SystemUnitClassType cable = null;
        foreach (var cab in productLib.SystemUnitClass)
        {
            if (cab.Name == productDetails.Name)
            {
                DeleteProduct(cab.ID);
                status = "Product updated.";
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
        var numberPins = 0;
        var pinIds = new List<string>(); // used later on for links
        foreach (var connectorInfo in productDetails.Connectors)
        {
            numberConnectors++;
            var connector = cable.ExternalInterface.Append(connectorInfo.Type);
            connector.RefBaseClassPath = connectorInfo.Path + "/" + connectorInfo.Type;

            // find connector in connector libs
            //var libPath = connectorInfo.Path.Remove(connectorInfo.Path.LastIndexOf("/"));
            var libPath = connectorInfo.Path;
            var connectorInLib = document.CAEXFile.getReferencedInterfaceClass(libPath);

            // add 1, 2, 3... to connector female, male
            numberPins = Int16.Parse(connectorInLib.ExternalInterface.Last.Name); // number of pins
            for (var i = 1; i <= numberPins; i++)
            {
                var pin = connector.ExternalInterface.Append(i.ToString());
                pinIds.Add(pin.ID);
            }
        }

        // add role class
        var roleClass = cable.SupportedRoleClass.Append();
        roleClass.RefRoleClassPath = "CableRCL/Cable";
        
        // add wiring
        var wireIds = new List<string>(); // used later on for links
        var wireDir = cable.InternalElement.Append("Wiring");

        // add C1, C2...
        for (var i = 1; i <= numberPins; i++)
        {
            var wire = wireDir.InternalElement.Append("C" + i);
            var wireRoleReq = wire.RoleRequirements.Append();
            wireRoleReq.RefBaseRoleClassPath = "CableRCL/Wire";

            wireIds.Add(wire.ID);

            // add P1, P2 for C1...
            for (var j = 1; j <= numberConnectors; j++)
            {
                var wirePin = wire.ExternalInterface.Append("P" + j);
            }
        }

        // add links
        var numberLinks = 0; // serves as help for naming links

        for (var i = 1; i <= numberConnectors; i++)
        {
            for (var j = 1; j <= numberPins; j++)
            {
                var wire = document.CAEXFile.FindCaexObjectFromId<InternalElementType>(wireIds[j-1]);
                var wirePinId = "";
                if (i == 1)
                {
                    wirePinId = wire.ExternalInterface[i-1].ID;
                }
                else
                {
                    wirePinId = wire.ExternalInterface[i-1].ID;
                }

                numberLinks++;
                var internalLink = cable.InternalLink.Append("InternalLink" + numberLinks);
                internalLink.RefPartnerSideA = pinIds[numberLinks-1];
                internalLink.RefPartnerSideB = wirePinId;
            }
        }
        
        // save aml file
        document.SaveToFile(workdir + AmlName, true);

        return status;
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
        
        foreach (var connector in unitFamilyType.ExternalInterface)
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

    private static ProductDetails GetWires(ProductDetails productDetails, SystemUnitFamilyType unitFamilyType, ref List<string> wirePinIds)
    {
        productDetails.Wires = new List<string>();
        //productDetails.Pins = new List<string>(); //not needed?!
        foreach (var wireClass in unitFamilyType.InternalElement)
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
    
    private static List<InterfaceFamilyType> DeepSearchInterfaceClass(InterfaceFamilyType interfaceClass)
    {
        if (interfaceClass.InterfaceClass.Count == 0)
        {
            // only add to list, if it's really a cable
            if (interfaceClass.Name.Contains("Female") || interfaceClass.Name.Contains("Male"))
            {
                return new List<InterfaceFamilyType>{interfaceClass};
            }
        }
    
        List<InterfaceFamilyType> results = new List<InterfaceFamilyType>();
        
        foreach (var inner in interfaceClass.InterfaceClass)
        {
            results = results.Concat(DeepSearchInterfaceClass(inner)).ToList();
        }
        
        return results;
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
}