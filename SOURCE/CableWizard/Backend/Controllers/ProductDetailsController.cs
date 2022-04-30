using CableWizardBackend.Models;
using Microsoft.AspNetCore.Mvc;

namespace CableWizardBackend.Controllers;

[ApiController]
<<<<<<< HEAD
[Route("api/v2/product-details/{id:regex(^[[a-z0-9]]*)}")]
=======
[Route("api/v2/product-details/{id}")]
>>>>>>> a70b876f44be87580e8c81ef9b04fe4817b83ced
public class ProductDetailsController : Controller
{
    [HttpGet(Name = "GetProductDetails")]
    
    public ProductDetails Get(string id)
    {
        //var product = AmlSerializer.GetProductDetails(id);
        return new ProductDetails
        {
            Id = "test",
            Name = "test2"
        };
    }
}