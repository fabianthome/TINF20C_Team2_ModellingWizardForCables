using Microsoft.AspNetCore.Mvc;

namespace CableWizardBackend.Controllers;

[ApiController]
[Route("api/v2/products")]
public class ProductController : Controller
{
    [HttpGet(Name = "GetProducts")]
    public string Get()
    {
        return AmlSerializer.GetProducts();
    }
}