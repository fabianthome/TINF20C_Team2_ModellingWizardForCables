using Aml.Engine.CAEX;
using Aml.Engine.Services;
using Microsoft.AspNetCore.Mvc;

namespace CableWizardBackend.Controllers;

[ApiController]
[Route("api/v2/download-file/{caexVersion:regex(^[[a-z0-9]]*)}")]
public class DownloadFileController : Controller
{
    [HttpGet(Name = "DownloadFile")]
    public ActionResult DownloadFile(string caexVersion)
    {
        string filePath = Directory.GetCurrentDirectory() + "/data/";
        string fileName = "Cables3_0.aml";
    
        // transform to caex 2.15 if needed
        if (caexVersion == "2_15")
        {
            fileName = AmlSerializer.ConvertFile();
        }

        byte[] fileBytes = System.IO.File.ReadAllBytes(filePath + fileName);
    
        return File(fileBytes, "application/force-download", fileName);
    }
}