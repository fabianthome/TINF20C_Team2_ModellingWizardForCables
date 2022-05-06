using CableWizardBackend.Models;
using Microsoft.AspNetCore.Mvc;

namespace CableWizardBackend.Controllers;

[ApiController]
[Route("api/v2/create-product/{libName}")]

public class CreateProductController : Controller
{
    [HttpPost(Name = "CreateProduct")]
    public string Create(string libName, [FromBody] ProductDetails productDetails)
    {
        AmlSerializer.CreateProduct(libName, productDetails);

        return "Created product.";
    }
    
}