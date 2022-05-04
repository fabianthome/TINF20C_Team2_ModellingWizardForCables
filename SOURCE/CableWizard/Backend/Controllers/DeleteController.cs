using Microsoft.AspNetCore.Mvc;

namespace CableWizardBackend.Controllers;

[ApiController]
[Route("api/v2/delete-product/{id:regex(^[[a-z0-9]]*)}")]

public class DeleteController : Controller
{
    [HttpDelete(Name = "DeleteProduct")]

    public string Delete(string id)
    {
        if (AmlSerializer.DeleteProduct(id))
        {
            return id + " deleted successfully";
        }

        return id + " could not be deleted";
    }
    
}