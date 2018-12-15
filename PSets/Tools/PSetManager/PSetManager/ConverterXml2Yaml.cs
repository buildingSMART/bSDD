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
    class ConverterXml2Yaml
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

        private bool CheckBSDD;
        private Bsdd _bsdd;

        private int numberOfPsets;
        private int numberOfPsetsWithbSDDGuid;
        private int numberOfProperties;
        private int numberOfPropertiesWithbSDDGuid;

        public ConverterXml2Yaml(string sourceFolderXml,string targetFolderYaml, string targetFolderJson, string targetFolderResx, bool checkBSDD = false)
        {

            if (sourceFolderXml==null)
            {
                log.Error($"ERROR - The parameter folderXml does not exist. Exiting!");
                return;
            }

            if (targetFolderYaml == null)
            {
                log.Error($"ERROR - The parameter folderYaml does not exist. Exiting!");
                return;
            }

            if (!Directory.Exists(sourceFolderXml))
            {
                log.Error($"ERROR - The Directory {sourceFolderXml} does not exist. Exiting!");
                return;
            }

            if (targetFolderYaml !=null)
               if (!Directory.Exists(targetFolderYaml))
                {
                    log.Error($"ERROR - The Directory {targetFolderYaml} does not exist. Exiting!");
                    return;
                }

            if (targetFolderJson != null)
                if (!Directory.Exists(targetFolderJson))
                {
                    log.Error($"ERROR - The Directory {targetFolderJson} does not exist. Exiting!");
                    return;
                }

            if (targetFolderResx != null)
                if (!Directory.Exists(targetFolderResx))
                {
                    log.Error($"ERROR - The Directory {targetFolderResx} does not exist. Exiting!");
                    return;
                }

            CheckBSDD = checkBSDD;
            if (CheckBSDD)
                _bsdd = new Bsdd();

            string propertySetVersionList = string.Empty;
            string propertySetTemplateList = string.Empty;
            string propertyTypeList = string.Empty;
            string propertyUnitList = string.Empty;

            foreach (string sourceFile in Directory.EnumerateFiles(sourceFolderXml, "PSet*.xml").OrderBy(x => x).ToList())//.Where(x=>x.Contains("Pset_CondenserTypeCommon")))
            {
                numberOfPsets++;

                string sourceFileContent = File.ReadAllText(sourceFile);
                //Dirty Fix of this serialization error: Not expected: <PropertySetDef xmlns='http://buildingSMART-tech.org/xml/psd/PSD_IFC4.xsd'> 
                string deserializationErrorString = "xmlns=\"http://buildingSMART-tech.org/xml/psd/PSD_IFC4.xsd\"";
                string sourceFileContentReplaced = sourceFileContent.Replace(deserializationErrorString, string.Empty);


                PropertySetDef pSet = PropertySetDef.Deserialize(sourceFileContentReplaced);
                log.Info("--------------------------------------------------");
                log.Info($"Checking PSet {pSet.Name}");
                log.Info($"Opened PSet-File {sourceFile.Replace(sourceFolderXml + @"\", string.Empty)}");

                if (!propertySetVersionList.Contains(pSet.IfcVersion.version))
                    propertySetVersionList += pSet.IfcVersion.version + ",";
                if (!propertySetTemplateList.Contains(pSet.templatetype.ToString()))
                    propertySetTemplateList += pSet.templatetype.ToString() + ",";

                PropertySet propertySet = new PropertySet()
                {
                    name = pSet.Name,
                    definition = pSet.Definition,
                    templatetype = pSet.templatetype.ToString()??string.Empty,
                    dictionaryReference = new DictionaryReference()
                    {
                        ifdGuid = pSet.ifdguid ?? string.Empty,
                        legacyGuid = pSet.ifdguid ?? string.Empty,
                        legacyGuidAsIfcGlobalId = Utils.GuidConverterToIfcGuid(pSet.ifdguid)
                    },
                    ifcVersion = new IfcVersion()
                    {
                        version = ConvertToSematicVersion(pSet.IfcVersion.version).ToString(),
                        schema = pSet.IfcVersion.schema
                    }                  
                };

                propertySet.applicableIfcClasses = new List<ApplicableIfcClass>();
                foreach (var applicableClass in pSet.ApplicableClasses)
                {
                    propertySet.applicableIfcClasses.Add(new ApplicableIfcClass()
                    {
                        name = applicableClass,
                        type = pSet.ApplicableTypeValue
                    });
                }

                //Insert missing standard localizations as dummys
                propertySet.localizations = new List<Localization>();
                foreach (string standardLanguage in StandardLanguages.OrderBy(x => x))
                    if (propertySet.localizations.Where(x => x.language == standardLanguage).FirstOrDefault() == null)
                        propertySet.localizations.Add(new Localization()
                        {
                            language = standardLanguage,
                            name = string.Empty,
                            definition = string.Empty
                        });

                if (CheckBSDD)
                {
                    if (propertySet.dictionaryReference.legacyGuid.Length == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        log.Info($"      ERROR: The GUID is missing in PSet!");
                        Console.ResetColor();
                    }
                    IfdConceptList ifdConceptList = _bsdd.SearchNests(pSet.Name);
                    if (ifdConceptList == null)
                    {
                        log.Info($"      Could not find the PSet in bSDD");
                    }
                    else
                    {
                        numberOfPsetsWithbSDDGuid++;
                        IfdConcept bsddPSet = ifdConceptList.IfdConcept.FirstOrDefault();
                        log.Info($"      Loaded Property from bSDD (1 out of {ifdConceptList.IfdConcept.Count})");
                        log.Info($"      Loaded PSet from bSDD");
                        log.Info($"         Guid:        {bsddPSet.Guid}");
                        log.Info($"         Status:      {bsddPSet.Status}");
                        log.Info($"         VersionDate: {bsddPSet.VersionDate}");
                        log.Info($"         Web:         http://bsdd.buildingsmart.org/#concept/browse/{bsddPSet.Guid}");

                        if (ifdConceptList.IfdConcept.Count == 1)
                        {
                            log.Info($"      The GUID of the PSet in the file was changed {propertySet.dictionaryReference.legacyGuid} => {bsddPSet.Guid}");
                            propertySet.dictionaryReference.ifdGuid = bsddPSet.Guid;
                        }
                    }
                }
                propertySet.dictionaryReference.dictionaryWebUri = $"http://bsdd.buildingsmart.org/#concept/browse/{propertySet.dictionaryReference.ifdGuid}";
                propertySet.dictionaryReference.dictionaryApiUri = $"http://bsdd.buildingsmart.org/api/4.0/IfdConcept/{propertySet.dictionaryReference.ifdGuid}";

                log.Info($"   Now checking the properties within the PSet");
                propertySet.properties = LoadProperties(pSet, pSet.PropertyDefs);
                propertySet = Utils.PrepareTexts(propertySet);

                if (targetFolderYaml != null)
                {
                    string targetFileYaml = sourceFile.Replace("xml", "YAML").Replace(sourceFolderXml, targetFolderYaml);

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

                    string yamlContent = yamlSerializer.Serialize(propertySet);
                    File.WriteAllText(targetFileYaml, yamlContent, Encoding.UTF8);

                    var yamlDeserializer = new DeserializerBuilder().Build();
                    try
                    {
                        propertySet = yamlDeserializer.Deserialize<PropertySet>(new StringReader(File.ReadAllText(targetFileYaml)));
                        log.Info("The YAML file is valid");
                    }
                    catch (Exception ex)
                    {
                        Console.Write("   ERROR!");
                        log.Info(ex.Message);
                    }

                    log.Info("The PSet was saved as YAML file");
                }


                if (targetFolderJson != null)
                {
                    string targetFileJson = sourceFile.Replace("xml", "json").Replace(sourceFolderXml, targetFolderJson);
                    JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings();
                    jsonSerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
                    jsonSerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                    string jsonContent = JsonConvert.SerializeObject(propertySet,Formatting.Indented, jsonSerializerSettings);
                    File.WriteAllText(targetFileJson, jsonContent, Encoding.UTF8);
                    log.Info("The PSet was saved as JSON file");
                }

                if (targetFolderResx != null)
                {
                    string targetFileResx = sourceFile.Replace("xml", "resx").Replace(sourceFolderXml, targetFolderResx);
                    ResxWriter resx = new ResxWriter(targetFileResx);
                    resx.Write(propertySet,StandardLanguages);
                    log.Info("The PSet was saved as RESX file");
                }
            }
            log.Info($"Number of PSets:                 {numberOfPsets}");
            log.Info($"   with not resolved bSDD Guid:  {numberOfPsetsWithbSDDGuid}");
            log.Info($"Number of Properties:            {numberOfProperties}");
            log.Info($"   with not resolved bSDD Guid:  {numberOfPropertiesWithbSDDGuid}");
        }

        private List<Property> LoadProperties(PropertySetDef pset, List<PropertyDef> PropertyDefs)
        {
            List<Property> properties = new List<Property>();
            foreach (PropertyDef psetProperty in PropertyDefs)
            {
                numberOfProperties++;
                log.Info("      .................");
                int itemNumber = 0;
                foreach (var item in psetProperty.Items)
                {
                    string ty = item.ToString();
                    if (item.ToString().Contains("PropertyType"))
                        break;
                    itemNumber++;
                }
                PropertyType psetValueType = (PropertyType)psetProperty.Items[itemNumber];
                PropertyTypeTypePropertyBoundedValue psetBoundedValue = null;
                PropertyTypeTypeComplexProperty psetComplexProperty = null;
                PropertyTypeTypePropertyEnumeratedValue psetEnumeratedValue = null;
                PropertyTypeTypePropertyListValue psetListValue = null;
                PropertyTypeTypePropertyReferenceValue psetReferenceValue = null;
                PropertyTypeTypePropertySingleValue psetSingleValue = null;
                PropertyTypeTypePropertyTableValue psetTableValue = null;

                string valueTypeAsString = psetValueType.Item.ToString().Replace("PSetManager.", "");

                Property property = new Property()
                {
                    name = psetProperty.Items[0].ToString(),
                    definition = psetProperty.Items[1].ToString(),
                    dictionaryReference = new DictionaryReference()
                    { 
                        dictionaryIdentifier = "http://bsdd.buildingsmart.org",
                        dictionaryNamespace = "PSet",                  
                        ifdGuid = "",
                        legacyGuid = psetProperty.ifdguid,
                        legacyGuidAsIfcGlobalId = Utils.GuidConverterToIfcGuid(psetProperty.ifdguid)
                    }                    
                };
                log.Info($"      Name: {property.name}");
                if (property.dictionaryReference.legacyGuidAsIfcGlobalId.Length == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    log.Info($"      ERROR: The GUID for {property.name} is missing in PSet!");
                    Console.ResetColor();
                }

                if (CheckBSDD)
                { 
                    if (GuidExistsInBsdd(property.dictionaryReference.legacyGuidAsIfcGlobalId) == false)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        log.Info($"      ERROR: The GUID {property.dictionaryReference.legacyGuidAsIfcGlobalId} for {property.name} is not resolved by http://bsdd.buildingsmart.org");
                        Console.ResetColor();

                        IfdConceptList ifdConceptList = _bsdd.SearchProperties(pset.Name + "." + property.name);
                        if (ifdConceptList == null)
                        {
                            log.Info($"      Could not find the Property in bSDD");
                        }
                        else
                        {
                            numberOfPropertiesWithbSDDGuid++;
                            IfdConcept bsddProperty = ifdConceptList.IfdConcept.FirstOrDefault();
                            log.Info($"      Loaded Property from bSDD (1 out of {ifdConceptList.IfdConcept.Count})");
                            log.Info($"         Guid:           {bsddProperty.Guid}");
                            log.Info($"         Status:         {bsddProperty.Status}");
                            log.Info($"         VersionDate:    {bsddProperty.VersionDate}");
                            log.Info($"         Web:            http://bsdd.buildingsmart.org/#concept/browse/{bsddProperty.Guid}");
                            foreach (var item in bsddProperty.FullNames)
                            {
                                int l = item.Language.LanguageCode.Length;
                                string tab = new string(' ', 10 - l);
                                log.Info($"         Name {item.Language.LanguageCode}:{tab}{item.Name}");
                            }
                            if (ifdConceptList.IfdConcept.Count == 1)
                            {
                                log.Info($"      The GUID in the PSet file was changed {property.dictionaryReference.legacyGuid} => {bsddProperty.Guid}");
                                property.dictionaryReference.ifdGuid = bsddProperty.Guid;
                            }
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        log.Info($"      OK: The GUID {property.dictionaryReference.legacyGuidAsIfcGlobalId} for {property.name} is properly resolved by http://bsdd.buildingsmart.org");
                        property.dictionaryReference.ifdGuid = property.dictionaryReference.legacyGuidAsIfcGlobalId;
                        Console.ResetColor();
                    }
                }
                property.dictionaryReference.dictionaryWebUri = $"http://bsdd.buildingsmart.org/#concept/browse/{property.dictionaryReference.ifdGuid}";
                property.dictionaryReference.dictionaryApiUri = $"http://bsdd.buildingsmart.org/api/4.0/IfdConcept/{property.dictionaryReference.ifdGuid}";
                property.localizations = new List<Localization>();
                PropertyDefNameAliases nameAliases = null;
                PropertyDefDefinitionAliases definitionAliases = null;
                if (psetProperty.Items.Length >= 4)
                {
                    foreach (var item in psetProperty.Items)
                        if (item.GetType() == typeof(PSetManager.PropertyDefNameAliases))
                            nameAliases = (PropertyDefNameAliases)item;

                    foreach (var item in psetProperty.Items)
                        if (item.GetType() == typeof(PSetManager.PropertyDefDefinitionAliases))
                            definitionAliases = (PropertyDefDefinitionAliases)item;

                    foreach (var alias in nameAliases.NameAlias)
                    {
                        property.localizations.Add(new Localization()
                        {
                            language = alias.lang,
                            name = alias.Value ?? string.Empty,
                            definition = definitionAliases.DefinitionAlias.Where(x => x.lang == alias.lang)?.FirstOrDefault()?.Value ?? string.Empty
                        });
                    }
                }

                //Insert missing standard localizations as dummys
                foreach (string standardLanguage in StandardLanguages.OrderBy(x=>x))
                    if (property.localizations.Where(x => x.language == standardLanguage).FirstOrDefault() == null)
                        property.localizations.Add(new Localization()
                        {
                            language = standardLanguage,
                            name = string.Empty,
                            definition = string.Empty
                        });

                IfcDataType resultParseIfcDataType;
                ReferenceClass resultParseReferenceClass;

                switch (valueTypeAsString)
                {
                    case "PropertyTypeTypePropertyBoundedValue":
                        psetBoundedValue = (PropertyTypeTypePropertyBoundedValue)psetValueType.Item;
                        property.typePropertyBoundedValue = new TypePropertyBoundedValue();
                        property.typePropertyBoundedValue.typeName = nameof(TypePropertyBoundedValue);
                        Enum.TryParse(psetSingleValue?.DataType?.type.ToString() ?? string.Empty, out resultParseIfcDataType);
                        property.typePropertyBoundedValue.dataType = resultParseIfcDataType;
                        property.typePropertyBoundedValue.unitType = psetBoundedValue.UnitType.type.ToString() ?? string.Empty;
                        property.typePropertyBoundedValue.LowerBoundValue = psetBoundedValue.ValueRangeDef.LowerBoundValue.value?.ToString() ?? string.Empty;
                        property.typePropertyBoundedValue.UpperBoundValue = psetBoundedValue.ValueRangeDef.UpperBoundValue.value?.ToString() ?? string.Empty;
                        break;
                    case "PropertyTypeTypeComplexProperty":
                        psetComplexProperty = (PropertyTypeTypeComplexProperty)psetValueType.Item;
                        property.typeComplexProperty = new TypeComplexProperty();
                        property.typeComplexProperty.name = psetComplexProperty.name ?? string.Empty;
                        property.typeComplexProperty.subProperties = LoadProperties(pset,psetComplexProperty.PropertyDef); //Recursiv loading
                        break;
                    case "PropertyTypeTypePropertyEnumeratedValue":
                        psetEnumeratedValue = (PropertyTypeTypePropertyEnumeratedValue)psetValueType.Item;

                        if (psetEnumeratedValue.EnumList.EnumItem.Count != 0)
                        { 
                            property.typePropertyEnumeratedValue = new TypePropertyEnumeratedValue();
                            property.typePropertyEnumeratedValue.listName = psetEnumeratedValue.EnumList.name;
                            property.typePropertyEnumeratedValue.enumerationValues = new List<EnumerationValue>();

                            foreach (var item in psetEnumeratedValue.EnumList.EnumItem)
                            {
                                EnumerationValue enumerationValue = new EnumerationValue()
                                {
                                    ifdGuid = string.Empty,// Utils.GuidConverterToIfcGuid(Guid.NewGuid().ToString()),
                                    name = Utils.CleanUp(item),
                                    definition = string.Empty,
                                    localizations = new List<Localization>()
                                };

                                foreach (string standardLanguage in StandardLanguages.OrderBy(x => x))
                                {
                                    enumerationValue.localizations.Add(new Localization()
                                    {
                                        language = standardLanguage,
                                        name = Utils.FirstUpperRestLower(item.ToString()),
                                        definition = string.Empty
                                    });
                                }

                                property.typePropertyEnumeratedValue.enumerationValues.Add(enumerationValue);
                            }
                        }

                        if (psetEnumeratedValue.ConstantList.ConstantDef.Count != 0)
                        {
                            property.typePropertyEnumeratedValue = new TypePropertyEnumeratedValue();
                            property.typePropertyEnumeratedValue.constantValues = new List<ConstantValue>();
                            //property.typePropertyEnumeratedValue.listName = psetEnumeratedValue.ConstantList.ConstantDef

                            foreach (var item in psetEnumeratedValue.ConstantList.ConstantDef)
                            {

                                string nameItem = item.Items[0].ToString();

                                string definitionItem;
                                if (item.Items[1].GetType() == typeof(String))
                                    definitionItem = item.Items[1].ToString();
                                else definitionItem = string.Empty;

                                ConstantValue constantValue = new ConstantValue()
                                {
                                    ifdGuid = string.Empty,
                                    name = Utils.CleanUp(nameItem),
                                    definition = Utils.CleanUp(definitionItem),
                                    localizations = new List<Localization>()
                                };

                                foreach (string standardLanguage in StandardLanguages.OrderBy(x => x))
                                {

                                    ConstantDefNameAliases constantDefNameAlias = null;
                                    foreach (var i in item.Items)
                                        if (i.GetType() == typeof(PSetManager.ConstantDefNameAliases))
                                            constantDefNameAlias = (ConstantDefNameAliases)i;

                                    ConstantDefDefinitionAliases constantDefDefinitionAliases = null;
                                    foreach (var i in item.Items)
                                        if (i.GetType() == typeof(PSetManager.ConstantDefDefinitionAliases))
                                            constantDefDefinitionAliases = (ConstantDefDefinitionAliases)i;

                                    //if ((constantDefNameAlias.NameAlias.FirstOrDefault().Value != null) || (constantDefDefinitionAliases.NameAlias.FirstOrDefault().Value != null))
                                    constantValue.localizations.Add(new Localization()
                                    {
                                        language = standardLanguage,
                                        name = Utils.FirstUpperRestLower(constantDefNameAlias.NameAlias.Where(l=>l.lang.ToLower()==standardLanguage.ToLower()).FirstOrDefault()?.Value ?? string.Empty),
                                        definition = Utils.FirstUpperRestLower(constantDefDefinitionAliases.DefinitionAlias.Where(l => l.lang.ToLower() == standardLanguage.ToLower()).FirstOrDefault()?.Value ?? string.Empty)
                                    });
                                }

                                property.typePropertyEnumeratedValue.constantValues.Add(constantValue);
                            }
                        }

                        break;
                    case "PropertyTypeTypePropertyListValue":
                        psetListValue = (PropertyTypeTypePropertyListValue)psetValueType.Item;
                        property.typePropertyListValue = new TypePropertyListValue();
                        Enum.TryParse(psetListValue?.ListValue.DataType?.type.ToString() ?? string.Empty, out resultParseIfcDataType);
                        property.typePropertyListValue.dataType = resultParseIfcDataType;
                        property.typePropertyListValue.unitType = psetListValue?.ListValue.UnitType?.type.ToString() ?? string.Empty;
                        property.typePropertyListValue.measureType = "";
                        if (psetListValue.ListValue.Values.ValueItem.Count>0)
                        { 
                            property.typePropertyListValue.listValues = new List<string>();
                            foreach (string valueItem in psetListValue.ListValue.Values.ValueItem)
                                property.typePropertyListValue.listValues.Add(valueItem);
                        }
                        break;
                    case "PropertyTypeTypePropertyReferenceValue":
                        psetReferenceValue = (PropertyTypeTypePropertyReferenceValue)psetValueType.Item;
                        property.typePropertyReferenceValue = new TypePropertyReferenceValue();

                        Enum.TryParse(psetReferenceValue.reftype.ToString() ?? string.Empty, out resultParseReferenceClass);
                        property.typePropertyReferenceValue.refType = resultParseReferenceClass;
                        if (property.typePropertyReferenceValue.refType.ToString().StartsWith("Ifc"))
                        {
                            property.typePropertyReferenceValue.guid = string.Empty;
                            property.typePropertyReferenceValue.url = "http://buildingsmart-tech.org";
                            property.typePropertyReferenceValue.libraryName = "Industry Foundation Classes by buildingSMART International";
                            property.typePropertyReferenceValue.sectionref = "Core Schema";
                        }
                        else
                        {
                            property.typePropertyReferenceValue.guid = string.Empty;
                            property.typePropertyReferenceValue.url = string.Empty;
                            property.typePropertyReferenceValue.libraryName = string.Empty;
                            property.typePropertyReferenceValue.sectionref = string.Empty;
                        }
                        break;
                    case "PropertyTypeTypePropertySingleValue":
                        psetSingleValue = (PropertyTypeTypePropertySingleValue)psetValueType.Item;
                        property.typePropertySingleValue = new TypePropertySingleValue();
                        Enum.TryParse(psetSingleValue?.DataType?.type.ToString() ?? string.Empty, out resultParseIfcDataType);
                        property.typePropertySingleValue.dataType = resultParseIfcDataType;
                        property.typePropertySingleValue.unitType = psetSingleValue?.UnitType?.type.ToString() ?? string.Empty;
                        property.typePropertySingleValue.measureType = "";
                        break;
                    case "PropertyTypeTypePropertyTableValue":
                        psetTableValue = (PropertyTypeTypePropertyTableValue)psetValueType.Item;
                        property.typePropertyTableValue = new TypePropertyTableValue();
                        property.typePropertyTableValue.Expression = psetTableValue.Expression;

                        property.typePropertyTableValue.DefiningValue = new TableDefValues();
                        Enum.TryParse(psetTableValue?.DefiningValue?.DataType?.type.ToString() ?? string.Empty, out resultParseIfcDataType);
                        property.typePropertyTableValue.DefiningValue.dataType = resultParseIfcDataType;
                        property.typePropertyTableValue.DefiningValue.unitType = psetTableValue?.DefiningValue.UnitType?.type.ToString() ?? string.Empty;
                        property.typePropertyTableValue.DefiningValue.measureType = "";

                        property.typePropertyTableValue.DefinedValue = new TableDefValues();
                        Enum.TryParse(psetTableValue?.DefinedValue.DataType?.type.ToString() ?? string.Empty, out resultParseIfcDataType);
                        property.typePropertyTableValue.DefinedValue.dataType = resultParseIfcDataType;
                        property.typePropertyTableValue.DefinedValue.unitType = psetTableValue?.DefiningValue.UnitType?.type.ToString() ?? string.Empty;
                        property.typePropertyTableValue.DefinedValue.measureType = "";
                        break;
                };

                property.status = new PublicationStatus()
                {
                    versionNumber = 4,                 
                    dateOfVersion = new DateTime(2018, 1, 1),
                    revisionNumber = 2,
                    dateOfRevision = new DateTime(2018, 1, 1),
                    status = PublicationStatus.Status.Active.ToString(),
                    dateOfCreation = new DateTime(2018, 1, 1),
                    dateOfActivation = new DateTime(2018, 1, 1),
                    dateOfLastChange = new DateTime(2018, 1, 1),
                    languageOfCreator = "en-EN"
                };


                properties.Add(property);
            }

            return properties;
        }

        private System.Version ConvertToSematicVersion(string classicIfcVersion)
        {
            System.Version version = new System.Version(0,0,0,0);

            switch (classicIfcVersion)
            { 
                case "IFC 2x3":
                    version = new System.Version(2, 3, 0, 0);
                    break;
                case "IFC4":
                    version = new System.Version(4, 0, 0, 0);
                    break;
            }

            return version;
        }

        private bool GuidExistsInBsdd(string guid)
        {
            //guid = "3WcEc0qRmHuO00025QrE$V"; working GUID
            bool check = false;
            string url = $"http://bsdd.buildingsmart.org/api/4.0/IfdConcept/{guid}";

            var webClient = new WebClient();
            try
            {
                var response = webClient.DownloadString(url);
                check = true;
            }
            catch(Exception ex)
            {
                //log.Info($"      ERROR: {ex.Message} {url}");
                check = false;
            }

            return check;
        }
    }
}
