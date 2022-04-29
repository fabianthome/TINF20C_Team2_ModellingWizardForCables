using CableWizardBackend.Models;
using Microsoft.AspNetCore.Mvc;

namespace CableWizardBackend.Controllers;

[ApiController]
[Route("api/v2/product-details/{id:int}")]
public class ProductDetailsController : Controller
{
    [HttpGet(Name = "GetProductDetails")]
    
    public ProductDetails Get(int id)
    {
        AmlSerializer.Test();

        return new ProductDetails
        {
            Id = "test",
            Name = "test2"
        };
    }
}