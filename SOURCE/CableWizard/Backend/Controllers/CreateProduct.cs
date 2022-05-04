using Microsoft.AspNetCore.Mvc;

namespace CableWizardBackend.Controllers;

[ApiController]
[Route("api/v2/create-product/{filename:regex(^[[a-z0-9]]*)}/{productDetails:regex(^[[a-z0-9]]*)}")]

public class CreateProduct : Controller
{
    [HttpPost(Name = "CreateProduct")]

    public void Post(string filename, string productDetails)
    {
        AmlSerializer.CreateProduct(filename, productDetails);
    }
}