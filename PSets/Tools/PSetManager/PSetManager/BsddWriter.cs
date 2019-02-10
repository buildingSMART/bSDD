using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using YamlDotNet.Core;
using YamlDotNet.Serialization;
using PSets5;
using Newtonsoft.Json;
using bSDD.NET;
using bSDD.NET.Model.Objects;

namespace PSets4
{
    class BsddWriter
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public BsddWriter()
        {

        }

        public int Workspace(string folderYaml, string bsddUrl, string bsddUser, string bsddPassword, string languageCode)
        {
            log.Info($"Upload the texts to concepts in the buildingSMART Data Dictionary (bSDD) from this source: {folderYaml}");
            if (folderYaml != null)
                if (!Directory.Exists(folderYaml))
                {
                    log.Error($"ERROR - The Directory {folderYaml} does not exist. Exiting!");
                    return 1;
                }

            Bsdd bsdd = new Bsdd(bsddUrl, bsddUser, bsddPassword);
            log.Info($"Successfully logged in, into bSDD at {bsddUrl}");

            var yamlFileNames = Directory.EnumerateFiles(folderYaml, "PSet*.YAML");//.Where(x => x.Contains("Pset_StairCommon"));

            //A dirty trick to get all PSets done within one hour (the build time of Appveyor is limited to one hour)
            //Travers randomly the list of the PSet in ascending or descending order

            Random rand = new Random();
            if (rand.Next(0, 2) == 0)
                yamlFileNames = yamlFileNames.OrderBy(x => x);
            else
                yamlFileNames = yamlFileNames.OrderByDescending(x => x);

            int ctPSets = 0;
            int ctProperties = 0;
            int ctPropertiesWithMissingTranslation = 0;
            int ctPropertiesWithMissingGuid = 0;


            foreach (string yamlFileName in yamlFileNames)
            {
                ctPSets++;
                log.Info($"--------------------------------------------------------------------------------------------------------");
                log.Info($"--------------------------------------------------------------------------------------------------------");
                log.Info($"Loading PSet {ctPSets}/{yamlFileNames.Count()}");

                var yamlDeserializer = new DeserializerBuilder().Build();
                PropertySet PSet;
                try
                {
                    PSet = yamlDeserializer.Deserialize<PropertySet>(new StringReader(File.ReadAllText(yamlFileName)));
                    log.Info($"Opened the YAML file {yamlFileName}");
                    log.Info($"--------------------------------------------------------------------------------------------------------");
                    log.Info($"--------------------------------------------------------------------------------------------------------");
                    log.Info($"Now checking the PSet {PSet.name} in the bSDD at {PSet.dictionaryReference.ifdGuid}");
                    IfdConcept pSetConcept = bsdd.GetConcept(PSet.dictionaryReference.ifdGuid);
 
                    if (pSetConcept != null)
                    {
                        log.Info($"Ok, the PSet lives here: {bsddUrl}/#concept/browse/{pSetConcept.Guid}");
                        log.Info($"Status: {pSetConcept.Status}");

                        //Localisations of the PSet
                        foreach (var localization in PSet.localizations
                                                        .Where(x=>x.language.ToLower()== languageCode.ToLower())
                                                        .Where(x=>x.name.Length > 0))
                        {
                            log.Info($"Publishing PSet {PSet.name} for language {localization.language}");

                            var existingFullName = pSetConcept.FullNames.Where(x => x.Language.LanguageCode.ToLower() == localization.language.ToLower()).FirstOrDefault();
                            //Dirty fix for https://github.com/buildingSMART/bSDD/issues/11
                            localization.name = localization.name.Replace("  ", " ");
                            if (existingFullName == null)
                            {
                                log.Info($"    Insert Name for concept => {localization.name}");
                                var answer = bsdd.InsertConceptName(pSetConcept.Guid, localization.language, localization.name);
                                log.Info($"    Succesfully inserted => {answer.Guid}");
                            }
                            else
                            {
                                log.Info($"    Existing Translation for name {existingFullName.Language.LanguageCode}");
                                log.Info($"    NameType : {existingFullName.NameType}");
                                if (existingFullName.Name != localization.name)
                                {
                                    log.Warn($"    Update: {existingFullName.Name}=>{localization.name}");
                                    var answer = bsdd.UpdateConceptName(pSetConcept.Guid, existingFullName.Guid, localization.language, localization.name);
                                    log.Warn($"    Succesfully update => {answer.Guid}");
                                }
                                else
                                    log.Info($"    No Update needed, the names are identical: {existingFullName.Guid}");
                            }

                            var existingDefinition = pSetConcept.Definitions.Where(x => x.Language.LanguageCode.ToLower() == localization.language.ToLower()).FirstOrDefault();
                            //Dirty fix for https://github.com/buildingSMART/bSDD/issues/11
                            localization.definition = localization.definition.Replace("  ", " ");
                            if (existingDefinition == null)
                            {
                                log.Warn($"    Insert description for concept : '{localization.definition}'");
                                var answer = bsdd.InsertConceptDefinition(pSetConcept.Guid, localization.language, localization.definition);
                                log.Warn($"    Succesfully inserted => {answer.Guid}");
                            }
                            else
                            {
                                log.Info($"    Existing Translation for description {existingDefinition.Language.LanguageCode}");
                                log.Info($"    DescriptionType : {existingDefinition.DescriptionType}");
                                if (existingDefinition.Description != localization.definition)
                                { 
                                    log.Warn($"    Update: {existingDefinition.Description}=>{localization.definition}");
                                    var answer = bsdd.UpdateConceptDefinition(pSetConcept.Guid, existingDefinition.Guid, localization.language, localization.definition);
                                    log.Warn($"    Succesfully update => {answer.Guid}");
                                }
                                else
                                    log.Info($"    No Update needed, the definitions are identical: {existingDefinition.Guid}");
                            }
                        }

                        //Now traversing the properties of the PSet
                        foreach (var property in PSet.properties)
                        {
                            log.Info($"--------------------------------------------------------------------------------------------------------");
                            ctProperties++;
                            log.Info($"Loading property #{ctProperties}");
                            IfdConcept propertyConcept = bsdd.GetConcept(property.dictionaryReference.ifdGuid);

                            if (propertyConcept != null)
                            {
                                log.Info($"Ok, the property '{property.name}' lives here: {bsddUrl}/#concept/browse/{propertyConcept.Guid}");
                                log.Info($"Status: {propertyConcept.Status}");

                                //Check, if the property is correctly related to its PSet
                                //If not, fix the relation
                                bool isRelated = bsdd.RelatePropertyToPSet(pSetConcept.Guid, propertyConcept.Guid);
                                if (isRelated)
                                    log.Info($"The property is correctly related to its PSet");
                                else
                                    log.Warn($"The relation of the property to its PSet was now inserted with the relationshipType='COLLECTS'.");

                                //Localizations of the Property

                                var propertyLocalizations = property.localizations
                                                                .Where(x => x.language.ToLower() == languageCode.ToLower())
                                                                .Where(x => x.name.Length > 0);

                                if (propertyLocalizations.Count()==0)
                                {
                                    ctPropertiesWithMissingTranslation++;
                                    log.Error($"ERROR: Translation missing for {PSet.name}.{property.name} into {languageCode}");
                                }
                                else
                                foreach (var localization in propertyLocalizations)
                                {
                                    log.Info($"Publishing the Property for language {localization.language}");

                                    {   //managing the names of the property

                                        //Dirty fix for https://github.com/buildingSMART/bSDD/issues/11
                                        localization.name = localization.name.Replace("  ", " ");

                                        var existingNames = propertyConcept.FullNames.Where(x => x.Language.LanguageCode.ToLower() == localization.language.ToLower()).ToList();
                                        if (existingNames.Count == 0)
                                        {
                                        log.Warn($"    No name exists for the language {localization.language}");
                                        log.Warn($"    Insert this name as the first name : '{localization.name}'");
                                            if (localization.name.Length > 0)
                                            {
                                                var answer = bsdd.InsertConceptName(propertyConcept.Guid, localization.language, localization.name);
                                                log.Warn($"    Succesfully inserted first name with GUID {answer.Guid} for the concept {propertyConcept.Guid}");
                                            }
                                            else
                                            {
                                                ctPropertiesWithMissingTranslation++; 
                                                log.Error($"    ERROR: Translation of name missing for {PSet.name}.{property.name} into {languageCode}");
                                            }
                                        }
                                        else
                                        {
                                            log.Info($"    There exists {existingNames.Count()} name(s) for the concept {propertyConcept.Guid} in the language {localization.language}");
                                            //INTERNAL REMARK:  In the context of IFC should only exist one name per concept and language, 
                                            //                  even when the data model of IFD allows more than one name
                                            //                  So, we check, if the name allready exists, and if not, 
                                            //                  then we insert the new name and delete all existing name for this language and concept
                                            //TECHNICAL REMARK: Since at leat one name mus exist, we have to insert first the new name, if is does not 
                                            //                  allready exist, and then delete all other names for this concept and language

                                            string guidOfExistingName = null;
                                            string guidOfNewName = null;
                                            if (existingNames.Select(x => x.Name).ToList().Contains(localization.name))
                                            {
                                                guidOfExistingName = existingNames.Where(x => x.Name == localization.name).FirstOrDefault().Guid;
                                                log.Info($"    The name already exists with the GUID {guidOfExistingName} : '{localization.name}'");
                                                log.Info($"    No insertion needed");

                                            }
                                            else
                                            {
                                                log.Warn($"    The name does not exist, inserting it now : '{localization.name}'");
                                                var answer = bsdd.InsertConceptName(propertyConcept.Guid, localization.language, localization.name);
                                                guidOfNewName = answer.Guid;
                                                log.Warn($"    Succesfully inserted with the GUID {guidOfNewName}");
                                            }
                                            foreach (var name in existingNames.Where(x => x.Guid != guidOfExistingName).Where(y => y.Guid != guidOfNewName))
                                            {
                                                log.Warn($"    Deleting old name with GUID {name.Guid} : '{name.Name}'");
                                                var answer = bsdd.DeleteConceptName(propertyConcept.Guid, name.Guid);
                                            }
                                        }
                                    }

                                    {   //Managing the definitions of the property

                                        //Dirty fix for https://github.com/buildingSMART/bSDD/issues/11
                                        localization.definition = localization.definition.Replace("  ", " ");

                                        var existingDefinitions = propertyConcept.Definitions.Where(x => x.Language.LanguageCode.ToLower() == localization.language.ToLower()).ToList();
                                        if (existingDefinitions.Count() == 0)
                                        {
                                            log.Warn($"    No description exists for the language {localization.language}");
                                            log.Warn($"    Insert this description as the first description : '{localization.definition}'");
                                            if (localization.definition.Length > 0)
                                            {
                                                var answer = bsdd.InsertConceptDefinition(propertyConcept.Guid, localization.language, localization.definition);
                                                log.Warn($"    Succesfully inserted first description with GUID {answer.Guid} for the concept {propertyConcept.Guid}");
                                            }
                                            else
                                            {
                                                ctPropertiesWithMissingTranslation++;
                                                log.Error($"    ERROR: Translation of definition missing for {PSet.name}.{property.name} into {languageCode}");
                                            }
                                        }
                                        else
                                        {
                                            log.Info($"    There exists {existingDefinitions.Count()} description(s) for the concept {propertyConcept.Guid} in the language {localization.language}");

                                            string guidOfExistingDefinition = null;
                                            string guidOfNewDefinition = null;
                                            if (existingDefinitions.Select(x => x.Description).ToList().Contains(localization.definition))
                                            {
                                                guidOfExistingDefinition = existingDefinitions.Where(x => x.Description == localization.definition).FirstOrDefault().Guid;
                                                log.Info($"    The description already exists with the GUID {guidOfExistingDefinition} : '{localization.definition}'");
                                                log.Info($"    No insertion needed");
                                            }
                                            else
                                            {
                                                log.Warn($"    The description does not exist, inserting it now : '{localization.definition}'");
                                                var answer = bsdd.InsertConceptDefinition(propertyConcept.Guid, localization.language, localization.definition);
                                                guidOfNewDefinition = answer.Guid;
                                                log.Warn($"    Succesfully inserted with the GUID {guidOfNewDefinition}");
                                            }
                                            foreach (var definition in existingDefinitions.Where(x => x.Guid != guidOfExistingDefinition).Where(y => y.Guid != guidOfNewDefinition))
                                            {
                                                log.Warn($"    Deleting old description with GUID {definition.Guid} : '{definition.Description}'");
                                                var answer = bsdd.DeleteConceptDescription(propertyConcept.Guid, definition.Guid);
                                            }
                                        }
                                    }
                                }
                                if (propertyConcept.Status != IfdStatusEnum.APPROVED)
                                { 
                                    bsdd.UpdateConceptStatus(propertyConcept.Guid, IfdStatusEnum.APPROVED);
                                    log.Warn($"    Succesfully updated the status of the Property concept {propertyConcept.Guid} to APPROVED");
                                }
                            }
                            else
                            {
                                ctPropertiesWithMissingGuid++;
                                log.Error($"ERROR: The property '{property.name}' cannot be found in the bSDD!");
                                if (property.dictionaryReference.ifdGuid.Length == 0)
                                {
                                    log.Error($"ERROR: The property '{property.name}' has no ifdGuid in the YAML file.");
                                    log.Warn($"Please search the GUID for the property in the bSDD and insert it into the YAML file.");
                                    log.Warn($"Then store the file to GitHub(e.g.with a pull request)");
                                    log.Warn($"ERROR: Now I am making a search for you on the bSDD for possible concepts with the term '{property.name}'");
                                    IfdConceptList possibleConcepts = bsdd.SearchConcepts(property.name);
                                    if (possibleConcepts==null)
                                        log.Warn($"    no terms found");
                                    else
                                    { 
                                        log.Warn($"    {possibleConcepts.IfdConcept.Count()} terms found, showing the first 10");
                                        foreach (var possibleConcept in possibleConcepts.IfdConcept.Where(x=>x.Definitions!=null).Take(20).OrderBy(x=>x.Status))
                                        {
                                            log.Warn($"    Found: {possibleConcept.Status} : {possibleConcept.ConceptType} - {possibleConcept.Guid} : {possibleConcept.Definitions.FirstOrDefault().Description}");
                                        }
                                        log.Warn($"{possibleConcepts.IfdConcept.Count()} Concepts found - You could pick up one of these for your property...");
                                    }
                                }
                            }
                        }
                        log.Info($"--------------------------------------------------------------------------------------------------------");
                        if (pSetConcept.Status != IfdStatusEnum.APPROVED)
                        {
                            bsdd.UpdateConceptStatus(pSetConcept.Guid, IfdStatusEnum.APPROVED);
                            log.Warn($"    Succesfully updated the status of the PSet concept {pSetConcept.Guid} to APPROVED");
                        }
                    }
                }
                catch (Exception ex)
                {
                    log.Error(ex.Message);
                    return 1;
                }
            }
            log.Warn($"Published {ctPSets} PSets with {ctProperties} Properties.");
            log.Warn($"{ctPropertiesWithMissingTranslation} translations missing.");
            log.Warn($"{ctPropertiesWithMissingGuid} guid missing.");

            return ctPropertiesWithMissingTranslation + ctPropertiesWithMissingGuid;
        }
    }
}