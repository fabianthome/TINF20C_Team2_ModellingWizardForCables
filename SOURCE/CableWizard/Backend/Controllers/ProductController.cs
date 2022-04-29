using CableWizardBackend.Models;
using Microsoft.AspNetCore.Mvc;

namespace CableWizardBackend.Controllers;

[ApiController]
[Route("api/v2/product-details/{id:int}")]
public class ProductDetailsController : Controller
{
    [HttpGet(Name = "GetProductDetails")]
    
    public CableDetails Get(int id)
    {
        AmlSerializer.Test();

        return new CableDetails
        {
            Id = "test",
            Name = "test2"
        };
    }
}