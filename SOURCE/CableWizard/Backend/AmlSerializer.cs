using Aml.Engine.AmlObjects;
using Aml.Engine.CAEX;
using Aml.Engine.CAEX.Extensions;

namespace CableWizardBackend;

public static class AmlSerializer
{
    private static readonly CAEXDocument Document;

    static AmlSerializer()
    {
        var filepath = $"{Directory.GetCurrentDirectory()}/Workdir/Cables_28032022.amlx";
        //todo get this path over environment variables
        var container = new AutomationMLContainer(filepath);
        Document = CAEXDocument.LoadFromStream(container.RootDocumentStream());
    }

    public static void Test()
    {
        var systemUnitClassLib = Document.CAEXFile.SystemUnitClassLib;

        foreach (var productLibrary in systemUnitClassLib)
        {
            Console.WriteLine(productLibrary);
        }

        /*
        var file = CAEXDocument.LoadFromFile("/Users/amtmann/Desktop/cable.aml");

        // browse the Instance Hierarchies in the file to import some elements
        foreach (var instanceHierarchy in file.CAEXFile.InstanceHierarchy)
        {
            Console.WriteLine(instanceHierarchy);
            // browse all InternalElements deep and import the internal Elements to your system
            foreach (var internalElement in instanceHierarchy.Descendants<InternalElementType>())
            {
                Console.WriteLine(internalElement);
                var test = internalElement.CAEXDocument.ToString();
                Console.WriteLine(test);
            }
        }
        */
    }
}