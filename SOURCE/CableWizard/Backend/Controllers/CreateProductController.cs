using CableWizardBackend.Models;
using Microsoft.AspNetCore.Mvc;

namespace CableWizardBackend.Controllers;

[ApiController]
[Route("api/v2/create-product")]

public class CreateProductController : Controller
{
    [HttpPost(Name = "CreateProduct")]
    public string Create([FromBody] ProductDetails productDetails)
    {
        return AmlSerializer.CreateProduct(productDetails);
    }
    
}