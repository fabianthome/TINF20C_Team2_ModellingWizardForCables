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
        return productDetails;
    }
}