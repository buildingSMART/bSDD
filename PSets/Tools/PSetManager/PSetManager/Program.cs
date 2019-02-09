using System;
using CommandLine;

namespace PSets4
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
                            ConverterXml2Yaml converterXml2Yaml = new ConverterXml2Yaml(options.folderXml, options.folderYaml, options.folderJson, options.folderResx, options.checkBSDD);
                            break;

                       case "LoadTranslation":
                            YamlTranslationWriter yamlTranslationWriter = new YamlTranslationWriter(options.translationSourceFile, options.folderYaml, options.folderJson, options.folderResx);
                            break;

                       case "PublishToBSDD":
                            BsddWriter bsddWriter = new BsddWriter(options.folderYaml, options.bsddUrl, options.bsddUser,options.bsddPassword, options.bsddLanguageCode);
                            break;
                   }
               });

            log.Info($"--------------------------------------------------------------------------------------------------");
            log.Info($"I am finished - Be happy with your Open BIM powered by the family of standards by buildingSMART International");
        }
    }
}
