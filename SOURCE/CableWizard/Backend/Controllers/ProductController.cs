using CableWizardBackend.Models;
using Microsoft.AspNetCore.Mvc;
using Aml.Engine.CAEX;
using Aml.Engine.CAEX.Extensions;

namespace CableWizardBackend.Controllers;

[ApiController]
[Route("api/v2/products")]
public class ProductController : Controller
{
    [HttpGet(Name = "GetProductInfos")]
    public IEnumerable<ProductInfo> Get()
    {
        
        var file = CAEXDocument.LoadFromFile("/Users/amtmann/Desktop/cable.aml");

        // browse the Instance Hierarchies in the file to import some elements
        foreach (var instanceHierarchy in file.CAEXFile.InstanceHierarchy)
        {
            // browse all InternalElements deep and import the internal Elements to your system
            foreach (var internalElement in instanceHierarchy.Descendants<InternalElementType>())
            {
                Console.WriteLine(internalElement);
                string test = internalElement.CAEXDocument.ToString();
                Console.WriteLine(test);
            }
        }

        var products = new[]
        {
            // for each CABLEINFOLDER make new ProductInfo { ID = Itterate, Name = Filename, Producer = AML.Engine.get.producer}
            new ProductInfo
            {
                Id = "1",
                Name = "example-cable",
                Producer = "Balluff"
            }, 
            new ProductInfo
            {
                Id = "2",
                Name = "Erdbeere",
                Producer = "Murr"
            }
        };
        return products;
    }
}