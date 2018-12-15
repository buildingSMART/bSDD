using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.Core;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
using PSD_IFC5;
using Newtonsoft.Json;
using System.Net;
using System.Xml.Serialization;
using bsDD.NET;
using bsDD.NET.Model.Objects;

namespace PSetManager
{
    class YamlTranslationWriter
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private List<string> StandardLanguages = new List<string>
            {
                "en-GB",
                "es-ES",
                "de-DE",
                "fr-FR",
                "ja-JP",
                "ru-RU"
            };

        public YamlTranslationWriter(string translationSourceFile, string folderYaml, string folderJson, string folderResx)
        {

            log.Info($"Inject the translation into the YAML files from this source table: {translationSourceFile}");
            if (translationSourceFile != null)
                log.Info($"Inject translations from {translationSourceFile}");
            if (folderYaml != null)
                log.Info($"Inject into YAML files in this target folder: {folderYaml}");
            if (folderJson != null)
                log.Info($"Inject into JSON files in this target folder: {folderJson}");
            if (folderResx != null)
                log.Info($"Inject into RESX files in this target folder: {folderResx}");

            if (translationSourceFile == null)
                {
                    log.Error($"ERROR - The parameter translationSourceFile does not exist. Exiting!");
                    return;
                }
            else
                if (!File.Exists(translationSourceFile))
                    {
                        log.Error($"ERROR - File {translationSourceFile} does not exist. Exiting!");
                        return;
                    }

            if (folderYaml == null)
                {
                    log.Error($"ERROR - The parameter folderXml does not exist. Exiting!");
                    return;
                }
            else
                if (!Directory.Exists(folderYaml))
                {
                    log.Error($"ERROR - The Directory {folderYaml} does not exist. Exiting!");
                    return;
                }

            if (folderJson != null)
                if (!Directory.Exists(folderJson))
                {
                    log.Error($"ERROR - The Directory {folderJson} does not exist. Exiting!");
                    return;
                }

            if (folderResx != null)
                if (!Directory.Exists(folderResx))
                {
                    log.Error($"ERROR - The Directory {folderResx} does not exist. Exiting!");
                    return;
                }


            foreach (Translation translation in ReadTranslationDataFromExcel(translationSourceFile))
            {
                string yamlFileName = Path.Combine(folderYaml, $"{translation.pset}.YAML");
                var yamlDeserializer = new DeserializerBuilder().Build();
                PropertySet propertySet;
                try
                {
                    propertySet = yamlDeserializer.Deserialize<PropertySet>(new StringReader(File.ReadAllText(yamlFileName)));
                    log.Info($"Opened the YAML file {yamlFileName}");
                }
                catch (Exception ex)
                {
                    log.Error(ex.Message);
                    return;
                }

                var ScalarStyleSingleQuoted = new YamlMemberAttribute()
                {
                    ScalarStyle = ScalarStyle.SingleQuoted
                };
                var yamlSerializer = new SerializerBuilder()
                //.WithNamingConvention(new CamelCaseNamingConvention())
                .WithAttributeOverride<PropertySet>(nc => nc.name, ScalarStyleSingleQuoted)
                .WithAttributeOverride<PropertySet>(nc => nc.definition, ScalarStyleSingleQuoted)
                .WithAttributeOverride<Localization>(nc => nc.name, ScalarStyleSingleQuoted)
                .WithAttributeOverride<Localization>(nc => nc.definition, ScalarStyleSingleQuoted)
                .Build();

                switch (translation.type)
                {
                    case "PSet":
                        log.Info($"Translate PSet {translation.pset} => {translation.name_TL} [{translation.language}]");
                        var localizationPSet = propertySet.localizations.Where(x => x.language.ToLower() == translation.language.ToLower()).FirstOrDefault();
                        localizationPSet.name = translation.name_TL;
                        localizationPSet.definition = translation.definition_tl;
                        string yamlContentPSet = yamlSerializer.Serialize(propertySet);
                        File.WriteAllText(yamlFileName, yamlContentPSet, Encoding.UTF8);
                        break;
                    case "Property":
                        log.Info($"Translated Property {translation.pset}.{translation.name} => {translation.name_TL} [{translation.language}]");
                        try
                        { 
                            var localizationProperty = propertySet.properties.Where(x=>x.name == translation.name).FirstOrDefault().localizations.Where(x => x.language.ToLower() == translation.language.ToLower()).FirstOrDefault();
                            localizationProperty.name = translation.name_TL;
                            localizationProperty.definition = translation.definition_tl;
                            string yamlContentProperty = yamlSerializer.Serialize(propertySet);
                            File.WriteAllText(yamlFileName, yamlContentProperty, Encoding.UTF8);
                        }
                        catch(Exception ex)
                        {
                            log.Error($"ERROR: {ex.Message}");
                        }
                        break;
                }

                if (folderJson != null)
                {
                    string targetFileJson = yamlFileName.Replace(".YAML", ".json").Replace(folderYaml, folderJson);
                    JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings();
                    jsonSerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
                    jsonSerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                    string jsonContent = JsonConvert.SerializeObject(propertySet, Formatting.Indented, jsonSerializerSettings);
                    File.WriteAllText(targetFileJson, jsonContent, Encoding.UTF8);
                    log.Info("The PSet was saved as JSON file");
                }

                if (folderResx != null)
                {
                    string targetFileResx = yamlFileName.Replace(".YAML", ".resx").Replace(folderYaml, folderResx);
                    ResxWriter resx = new ResxWriter(targetFileResx);
                    resx.Write(propertySet, StandardLanguages);
                    log.Info("The PSet was saved as RESX file");
                }
            }
        }


        private List<Translation> ReadTranslationDataFromExcel(string fileName) 
        {
            bool fileIsValid = true;
            ClosedXML.Excel.XLWorkbook workbook;

            try
            { 
              workbook = new ClosedXML.Excel.XLWorkbook(fileName);
            }
            catch(Exception ex)
            {
                log.Error($"ERROR: {ex.Message}");
                return null;
            }

            log.Info($"Excel file opened: {fileName}");
            var worksheet = workbook.Worksheets.FirstOrDefault();
            log.Info($"Loading translation texts from sheet {worksheet.Name}");
            log.Info($"Now checking, if the sheet contains the correct template...");

            const int idColumnPSet = 1;
            const int idColumnType = 2;
            const int idColumnIfdGuid = 3;
            const int idColumnIfcGlobalId = 4;
            const int idColumnName = 5;
            const int idColumnDefinition = 6;
            const int idColumnLanguage = 7;
            const int idColumnName_TL = 8;
            const int idColumnDefinition_TL = 9;

            if (worksheet.Cell(1, idColumnPSet).Value.ToString() != "PSet")
            {
                log.Error($"  cell A1 has not the name \"PSet\" ok");
                fileIsValid = false;
            }

            if (worksheet.Cell(1, idColumnType).Value.ToString() != "Type")
            {
                log.Error($"  cell B2 has not the name \"Type\" ok");
                fileIsValid = false;
            }

            if (worksheet.Cell(1, idColumnIfdGuid).Value.ToString() != "IfdGuid")
            {
                log.Error($"  cell C1 has not the name \"IfdGuid\" ok");
                fileIsValid = false;
            }

            if (worksheet.Cell(1, idColumnIfcGlobalId).Value.ToString() != "IfcGlobalId")
            {
                log.Error($"  cell D1 has not the name \"IfcGlobalId\" ok");
                fileIsValid = false;
            }

            if (worksheet.Cell(1, idColumnName).Value.ToString() != "Name")
            {
                log.Error($"  cell E1 has not the name \"Name\" ok");
                fileIsValid = false;
            }

            if (worksheet.Cell(1, idColumnDefinition).Value.ToString() != "Definition")
            {
                log.Error($"  cell F1 has not the name \"Definition\" ok");
                fileIsValid = false;
            }

            if (worksheet.Cell(1, idColumnLanguage).Value.ToString() != "Language")
            {
                log.Error($"  cell G1 has not the name \"Language\" ok");
                fileIsValid = false;
            }

            if (worksheet.Cell(1, idColumnName_TL).Value.ToString() != "Name_TL")
            {
                log.Error($"  cell H1 has not the name \"Name_TL\" ok");
                fileIsValid = false;
            }

            if (worksheet.Cell(1, idColumnDefinition_TL).Value.ToString() != "Definition_TL")
            {
                log.Error($"  cell I1 has not the name \"Definition_TL\" ok");
                fileIsValid = false;
            }

            if (!fileIsValid)
            {
                log.Error($"Please correct you template and try again...");
                return null;
            }

            log.Info($"The file has correct template - well done");

            var firstRowUsed = worksheet.FirstRowUsed();
            var translationRow = firstRowUsed.RowUsed();
            translationRow = translationRow.RowBelow(); // Move to the next row (it now has the headers)

            List<Translation> translations = new List<Translation>(); 
            while (!translationRow.Cell(idColumnType).IsEmpty())
            {
                translations.Add(new Translation()
                {
                    pset = translationRow.Cell(idColumnPSet).GetString(),
                    type = translationRow.Cell(idColumnType).GetString(),
                    ifdGuid = translationRow.Cell(idColumnIfdGuid).GetString(),
                    ifcGlobalId = translationRow.Cell(idColumnIfcGlobalId).GetString(),
                    name = translationRow.Cell(idColumnName).GetString(),
                    definition = translationRow.Cell(idColumnDefinition).GetString(),
                    language = translationRow.Cell(idColumnLanguage).GetString(),
                    name_TL = translationRow.Cell(idColumnName_TL).GetString(),
                    definition_tl = translationRow.Cell(idColumnDefinition_TL).GetString()
                });

                translationRow = translationRow.RowBelow();
            }
            return translations;
        }


    }
}