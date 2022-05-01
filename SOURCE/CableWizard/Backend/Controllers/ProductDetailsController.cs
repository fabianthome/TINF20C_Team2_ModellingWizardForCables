using CableWizardBackend.Models;
using Microsoft.AspNetCore.Mvc;

namespace CableWizardBackend.Controllers;

[ApiController]
[Route("api/v2/product-details/{id:regex(^[[a-z0-9]]*)}")]
public class ProductDetailsController : Controller
{
    [HttpGet(Name = "GetProductDetails")]
    
    public ProductDetails Get(string id)
    {
        var productDetails = AmlSerializer.GetProductDetails(id);
        return new ProductDetails
        {
            Id = id,
            Name = productDetails.Name,
            Library = productDetails.Library,
            Connectors = productDetails.Connectors,
            Wires = productDetails.Wires,
            Pins = productDetails.Pins,
            Manufacturer = productDetails.Manufacturer,
            ManufacturerURI = productDetails.ManufacturerURI,
            DeviceClass = productDetails.DeviceClass,
            Model = productDetails.Model,
            ProductCode = productDetails.ProductCode,
            TemperatureMin = productDetails.TemperatureMin,
            TemperatureMax = productDetails.TemperatureMax,
            IPCode = productDetails.IPCode,
            Material = productDetails.Material,
            Weight = productDetails.Weight,
            Height = productDetails.Height,
            Width = productDetails.Width,
            Length = productDetails.Length
        };
    }
}