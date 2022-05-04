using Microsoft.AspNetCore.Mvc;

namespace CableWizardBackend.Controllers;

[ApiController]
[Route("api/v2/create-product/{id:regex(^[[a-z0-9]]*)}")]

public class CreateProduct : Controller
{
    [HttpGet(Name = "CreateProduct")]

    public void Get(string id)
    {
        AmlSerializer.CreateProduct(id);
    }
}