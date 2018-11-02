using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using YamlDotNet.RepresentationModel;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace PSet2YamlConverter
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"PSet Manager started");
            Console.WriteLine($"A tool by the community of buildingSMART International");
            Console.WriteLine($"The home of PSet Manager is https://github.com/buildingsmart/bSDD");

            string sourceFolderXml = args[0];
            string targetFolderYaml = args[1];
            string targetFolderJson = args[2];
            Console.WriteLine($"Converting the PSets from this source folder: {sourceFolderXml}");
            Console.WriteLine($"Into YAML files in this target folder: {targetFolderYaml}");
            Console.WriteLine($"Into JSON files in this target folder: {targetFolderJson}");

            Converter converter = new Converter(sourceFolderXml, targetFolderYaml, targetFolderJson, true);

            Console.WriteLine($"Successfully finished - Be happy with your Open BIM");
        }
    }
}
