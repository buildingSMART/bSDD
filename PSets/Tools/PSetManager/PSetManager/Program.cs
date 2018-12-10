using System;
using CommandLine;

namespace PSetManager
{
    class Program
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        static void Main(string[] args)
        {
            log.Info($"PSet Manager started");
            log.Info($"A tool by the community of buildingSMART International");
            log.Info($"The home of PSet Manager is https://github.com/buildingsmart/bSDD");

            Parser.Default.ParseArguments<CommandLineOptions>(args)
               .WithParsed<CommandLineOptions>(options =>
               {
                   switch (options.mode)
                   {
                        case "ConvertFromXml":
                           log.Info($"Converting the PSets from this source folder: {options.folderXml}");
                           if (options.folderYaml !=null)
                              log.Info($"Into YAML files in this target folder: {options.folderYaml}");
                           if (options.folderJson != null)
                               log.Info($"Into JSON files in this target folder: {options.folderJson}");
                           if (options.folderResx != null)
                               log.Info($"Into Resx files in this target folder: {options.folderResx}");

                           ConverterXml2Yaml converterXml2Yaml = new ConverterXml2Yaml(options.folderXml, options.folderYaml, options.folderJson, options.folderResx, options.checkBSDD);
                        break;

                       case "LoadTranslation":
                           log.Info($"Load the translation into the YAML files from this source table: {options.folderYaml}");
                           if (options.translationSourceFile != null)
                               log.Info($"Load translation from {options.translationSourceFile}");
                           if (options.folderYaml != null)
                               log.Info($"Save into YAML files in this target folder: {options.folderYaml}");
                           YamlTranslationWriter yamlTranslationWriter = new YamlTranslationWriter(options.translationSourceFile, options.folderYaml);

                           break;
                   }
               });

            log.Info($"I am finished - Be happy with your Open BIM");
        }
    }
}
