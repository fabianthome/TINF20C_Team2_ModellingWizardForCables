using Microsoft.AspNetCore.Mvc;

namespace CableWizardBackend.Controllers;

[ApiController]
[Route("api/v2/download-file")]
public class DownloadFileController : Controller
{
    [HttpGet(Name = "DownloadFile")]
    public ActionResult DownloadFile()
    {
        string filePath = Directory.GetCurrentDirectory() + "/data/";
        string fileName = "Template.aml";
    
        byte[] fileBytes = System.IO.File.ReadAllBytes(filePath + fileName);
    
        return File(fileBytes, "application/force-download", fileName);
    }
}