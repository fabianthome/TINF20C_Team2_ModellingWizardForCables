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
        var products = new[]
        {
            new ProductInfo
            {
                Id = "1",
                Name = "example-cable",
                Producer = "Balluff"
            }
        };
        return products;
    }
}