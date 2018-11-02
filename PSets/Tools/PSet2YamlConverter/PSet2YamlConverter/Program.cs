using System;

namespace PSet2YamlConverter
{
    class Program
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        static void Main(string[] args)
        {
            log.Info($"PSet Manager started");
            log.Info($"A tool by the community of buildingSMART International");
            log.Info($"The home of PSet Manager is https://github.com/buildingsmart/bSDD");

            string sourceFolderXml = args[0];
            string targetFolderYaml = args[1];
            string targetFolderJson = args[2];
            log.Info($"Converting the PSets from this source folder: {sourceFolderXml}");
            log.Info($"Into YAML files in this target folder: {targetFolderYaml}");
            log.Info($"Into JSON files in this target folder: {targetFolderJson}");

            Converter converter = new Converter(sourceFolderXml, targetFolderYaml, targetFolderJson, true);

            log.Info($"Successfully finished - Be happy with your Open BIM");
        }
    }
}
