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
                              log.Info($"Converting YAML files in this target folder: {options.folderYaml}");
                           if (options.folderJson != null)
                               log.Info($"Converting JSON files in this target folder: {options.folderJson}");
                           if (options.folderResx != null)
                               log.Info($"Converting Resx files in this target folder: {options.folderResx}");

                           ConverterXml2Yaml converterXml2Yaml = new ConverterXml2Yaml(options.folderXml, options.folderYaml, options.folderJson, options.folderResx, options.checkBSDD);
                        break;

                       case "LoadTranslation":
                           log.Info($"Inject the translation into the YAML files from this source table: {options.folderYaml}");
                           if (options.translationSourceFile != null)
                               log.Info($"Inject translations from {options.translationSourceFile}");
                           if (options.folderYaml != null)
                               log.Info($"Inject into YAML files in this target folder: {options.folderYaml}");
                           if (options.folderJson != null)
                               log.Info($"Inject into JSON files in this target folder: {options.folderJson}");
                           if (options.folderResx != null)
                               log.Info($"Inject into RESX files in this target folder: {options.folderResx}");
                           YamlTranslationWriter yamlTranslationWriter = new YamlTranslationWriter(options.translationSourceFile, options.folderYaml, options.folderJson, options.folderResx);

                           break;
                   }
               });

            log.Info($"I am finished - Be happy with your Open BIM");
        }
    }
}
