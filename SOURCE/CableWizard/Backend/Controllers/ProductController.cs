using CableWizardBackend.Models;
using Microsoft.AspNetCore.Mvc;

namespace CableWizardBackend.Controllers;

[ApiController]
[Route("api/v2/products")]
public class ProductController : Controller
{
    [HttpGet(Name = "GetProductInfos")]
    public IEnumerable<ProductInfo> Get()
    {
        AmlSerializer.Test();
        
        var products = new[]
        {
            // for each CABLEINFOLDER make new ProductInfo { ID = Itterate, Name = Filename, Producer = AML.Engine.get.producer}
            new ProductInfo
            {
                Id = "1",
                Name = "example-cable",
                Producer = "Balluff"
            }, 
            new ProductInfo
            {
                Id = "2",
                Name = "Erdbeere",
                Producer = "Murr"
            }
        };
        return products;
    }
}