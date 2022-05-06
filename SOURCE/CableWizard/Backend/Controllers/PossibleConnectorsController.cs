using CableWizardBackend.Models;
using Microsoft.AspNetCore.Mvc;

namespace CableWizardBackend.Controllers;

[ApiController]
[Route("api/v2/possible-connectors")]

public class PossibleConnectorsController : Controller
{
    [HttpGet(Name = "GetPossibleConnectors")]
    public List<Tuple<string, string>> Get()
    {
        return AmlSerializer.GetPossibleConnectors();
    }
}