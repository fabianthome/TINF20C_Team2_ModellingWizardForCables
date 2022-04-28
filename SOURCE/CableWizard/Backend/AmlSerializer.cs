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

        var products = new List<string>();

        foreach (var productLibrary in systemUnitClassLib)
        {
            Console.WriteLine($"Product library: {productLibrary}");
            foreach (var systemUnitFamilyType in productLibrary.SystemUnitClass)
            {
                var list = DeepSearch(systemUnitFamilyType);
                foreach (var unitFamilyType in list)
                {
                    Console.WriteLine($"Cable: {unitFamilyType}");
                }
            }
        }
    }

    private static List<SystemUnitFamilyType> DeepSearch(SystemUnitFamilyType familyType)
    {
        if (familyType.SystemUnitClass.Count == 0)
        {
            return new List<SystemUnitFamilyType> {familyType};
        }

        List<SystemUnitFamilyType> results = new List<SystemUnitFamilyType>();

        foreach (var inner in familyType.SystemUnitClass)
        {
            results = results.Concat(DeepSearch(inner)).ToList();
        }

        return results;
    }
}