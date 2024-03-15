using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using XMLdemo.Model;

namespace XmlDemo;

internal static class Program
{
    private static void Main(string[] args)
    {
        const string path = "asodu.omx";

        // EXAMPLE 1
        ElementLoad(path);

        // EXAMPLE 2
        var doc          = new XmlDocument();
        var xmlInputData = File.ReadAllText(path);
        doc.LoadXml(xmlInputData);
        Console.WriteLine("----------------------------------------------------------------------------------------------------");
        RecursiveRead(doc.DocumentElement!);

        // EXAMPLE 3
        AttributeRead(doc.DocumentElement!);

        // EXAMPLE 4
        Console.WriteLine("----------------------------------------------------------------------------------------------------");
        var asoduDoc = XElement.Load(path);
        PrintElementV1(asoduDoc, 1);

        // EXAMPLE 5
        Console.WriteLine("----------------------------------------------------------------------------------------------------");
        PrintElementV2(asoduDoc);

        // EXAMPLE 6
        Console.WriteLine("----------------------------------------------------------------------------------------------------");
        PrintElementV3(path);

        // EXAMPLE 7
        Console.WriteLine("----------------------------------------------------------------------------------------------------");
        PrintElementV4(asoduDoc);

        // EXAMPLE 8
        Console.WriteLine("----------------------------------------------------------------------------------------------------");
        PrintElementV5(path);
    }

    private static void ElementLoad(string path)
    {
        var r = XmlReader.Create("asodu.omx");

        while (r.NodeType != XmlNodeType.Element)
            r.Read();

        var e = XElement.Load(r);

        Console.WriteLine("----------------------------------------------------------------------------------------------------");
        Console.WriteLine(e);
    }

    private static void AttributeRead(XmlNode node)
    {
        Console.WriteLine("----------------------------------------------------------------------------------------------------");

        foreach (var att in node.Attributes!.Cast<XmlAttribute>())
        {
            var text = att.Name + ": " + att.Value;
            Console.WriteLine(text);
        }
    }

    private static string? RecursiveRead(XmlNode node)
    {
        var children = new List<string>();

        if (node.HasChildNodes)
        {
            children.AddRange(from XmlNode child in node select RecursiveRead(child));

            Console.WriteLine("table : {0}; children : {1}; value : {2}", node.Name, string.Join(",", children.ToArray()),
                node.Value);
        }
        else
        {
            return node.Value;
        }

        return node.Name;
    }

    private static void PrintElementV1(XElement elem, int num)
    {
        if (elem.Elements().Any())
        {
            Console.WriteLine("{0}. {1}", num, elem.Name);
            var i = 1;
            foreach (var e in elem.Elements())
            {
                PrintElementV1(e, i++);
            }
        }
        else
        {
            Console.WriteLine("{0}: {1}", elem.Name, elem.Value);
        }
    }

    private static void PrintElementV2(XContainer elem)
    {
        var i = 1;
        foreach (var e in elem.Elements())
        {
            Console.WriteLine("{0}. {1}", i++, e.Name);
            PrintContent(e);
        }
    }

    private static void PrintContent(XContainer elem)
    {
        foreach (var e in elem.Elements())
        {
            Console.WriteLine("{0}: {1}", e.Name, e.Value);
        }
    }

    private static void PrintElementV3(string path)
    {
        var xd = XDocument.Load(path);

        foreach (var x in xd.Elements().Descendants())
        {
            if (x.HasElements)
                Console.WriteLine("\n{0}\n", x.Name);
            else
                Console.WriteLine("{0}\t{1}", x.Name, x.Value);
        }
    }

    private static void PrintElementV4(XElement elem)
    {
        foreach (var node in elem.Nodes())
        {
            foreach (var nod in node.ElementsAfterSelf())
            {
                Console.WriteLine(nod.FirstAttribute!.Value);
            }
        }
    }

    private static void PrintElementV5(string path)
    {
        List<Omx> listAllEntries = new();

        try
        {
            var serializer = new XmlSerializer(typeof(Omx));
            using (TextReader reader = new StringReader(System.IO.File.ReadAllText(path)))
            {
                var result = (Omx)serializer.Deserialize(reader);
                listAllEntries.Add(result);
                var applicationObjectChild = result.Domain.Computer.ExternalRuntime.ApplicationObject.Child;

                PrintNodes(applicationObjectChild, 1);
            }
        }
        catch (Exception ex)
        {
            // ignored
        }
    }

    private static void PrintNodes(Child child, int level)
    {
        Console.WriteLine("level: {0}, tag: {1}, title: {2}, description: {3}",
            level.ToString(),
            child.Name,
            child.Attribute[0].Value,
            child.Attribute[1].Value);

        foreach (var c in child.Children)
        {
            PrintNodes(c, level + 1);
        }
    }
}