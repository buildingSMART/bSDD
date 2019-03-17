using PSets5;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.Core;
using YamlDotNet.Serialization;

namespace PSetManager
{
    class Normalization
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public Normalization(string folderYaml)
        {
            log.Info($"Upload the texts to concepts in the buildingSMART Data Dictionary (bSDD) from this source: {folderYaml}");
            if (folderYaml != null)
                if (!Directory.Exists(folderYaml))
                {
                    log.Error($"ERROR - The Directory {folderYaml} does not exist. Exiting!");
                    return;
                }

            var psetFileNames = Directory.EnumerateFiles(folderYaml, "PSet*.YAML");//.Where(x => x.Contains("Pset_ActionRequest"));

            foreach (string psetFileName in psetFileNames)
            {
                log.Info($"--------------------------------------------------------------------------------------------------------");
                log.Info($"--------------------------------------------------------------------------------------------------------");
                var yamlDeserializer = new DeserializerBuilder().Build();

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

                PropertySet pSet;
                try
                {
                    pSet = yamlDeserializer.Deserialize<PropertySet>(new StringReader(File.ReadAllText(psetFileName)));
                    log.Info($"Opened the YAML file {psetFileName}");
                    log.Info($"--------------------------------------------------------------------------------------------------------");
                    log.Info($"--------------------------------------------------------------------------------------------------------");
                    log.Info($"Now checking the PSet {pSet.name} in the bSDD at {pSet.dictionaryReference.ifdGuid}");

                    if (pSet.dictionaryReference.legacyGuids is null)
                        pSet.dictionaryReference.legacyGuids = new List<string>();
                    if (!pSet.dictionaryReference.legacyGuids.Contains(pSet.dictionaryReference.legacyGuidAsIfcGlobalId))
                        pSet.dictionaryReference.legacyGuids.Add(pSet.dictionaryReference.legacyGuidAsIfcGlobalId);
                    if (pSet.propertyUsages is null)
                        pSet.propertyUsages = new List<PropertyUsage>();

                    foreach (var property in pSet.properties)
                    {
                        if (property.dictionaryReference.legacyGuids is null)
                            property.dictionaryReference.legacyGuids = new List<string>();
                        if (!property.dictionaryReference.legacyGuids.Contains(property.dictionaryReference.legacyGuidAsIfcGlobalId))
                            property.dictionaryReference.legacyGuids.Add(property.dictionaryReference.legacyGuidAsIfcGlobalId);

                        string subFolderName = Path.Combine(Path.Combine(folderYaml, "Properties", property.name.Substring(0, 1)));
                        string propertyFileName = Path.Combine(subFolderName, property.name+".YAML");
                        string yamlContentProperty = yamlSerializer.Serialize(property);
                        if (!Directory.Exists(subFolderName))
                            Directory.CreateDirectory(subFolderName);
                        if (File.Exists(propertyFileName))
                        {
                            Property existingProperty = yamlDeserializer.Deserialize<Property>(new StringReader(File.ReadAllText(propertyFileName)));

                            if (!existingProperty.dictionaryReference.legacyGuids.Contains(property.dictionaryReference.ifdGuid))
                            { 
                                existingProperty.dictionaryReference.legacyGuids.Add(property.dictionaryReference.ifdGuid);
                                string yamlContentExistingProperty = yamlSerializer.Serialize(existingProperty);
                                File.WriteAllText(propertyFileName, yamlContentExistingProperty, Encoding.UTF8);
                            }
                        }
                        else
                            File.WriteAllText(propertyFileName, yamlContentProperty, Encoding.UTF8);
 
                        string usageGuid = PSets4.GuidConverter.ConvertToIfcGuid(Guid.NewGuid());
                        pSet.propertyUsages.Add(new PropertyUsage()
                        {
                            propertyName = property.name,
                            usageDefinition = property.definition,
                            dictionaryReference = new DictionaryReference()
                            {
                               dictionaryIdentifier = "http://bsdd.buildingsmart.org",
                               dictionaryNamespace = "PSet",
                               dictionaryWebUri =  $"http://bsdd.buildingsmart.org/#concept/browse/{usageGuid}",
                               dictionaryApiUri = $"http://bsdd.buildingsmart.org/api/4.0/IfdConcept/{usageGuid}",
                               ifdGuid = usageGuid
                            }
                        });

                        
                    }

                    //PSet.properties = null;

                    string yamlContentPSet = yamlSerializer.Serialize(pSet);
                    File.WriteAllText(psetFileName, yamlContentPSet, Encoding.UTF8);
                }
                catch (Exception ex)
                {
                    log.Error(ex.Message);
                    return;
                }
            }
        }
    }
}
