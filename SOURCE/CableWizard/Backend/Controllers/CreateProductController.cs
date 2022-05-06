using CableWizardBackend.Models;
using Microsoft.AspNetCore.Mvc;

namespace CableWizardBackend.Controllers;

[ApiController]
[Route("api/v2/create-product/{filename}")]

public class CreateProductController : Controller
{
    [HttpPost(Name = "CreateProduct")]
    public string Create(string filename, [FromBody] ProductDetails productDetails)
    {
        AmlSerializer.CreateProduct(filename, productDetails);

        return "Created product.";
    }
    
}