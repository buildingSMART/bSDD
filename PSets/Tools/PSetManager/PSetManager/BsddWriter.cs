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

        public BsddWriter(string folderYaml, string bsddUrl, string bsddUser, string bsddPassword, string languageCode)
        {
            log.Info($"Upload the texts to concepts in the buildingSMART Data Dictionary (bSDD) from this source: {folderYaml}");

            if (folderYaml != null)
                if (!Directory.Exists(folderYaml))
                {
                    log.Error($"ERROR - The Directory {folderYaml} does not exist. Exiting!");
                    return;
                }

            Bsdd bsdd = new Bsdd(bsddUrl, bsddUser, bsddPassword);
            log.Info($"Successfully logged in, into bSDD at {bsddUrl}");
            foreach (string yamlFileName in Directory.EnumerateFiles(folderYaml, "PSet*.YAML").OrderBy(x => x).ToList().Where(x=>x.Contains("Pset_ActionRequest")))
            { 
                var yamlDeserializer = new DeserializerBuilder().Build();
                PropertySet PSet;
                try
                {
                    PSet = yamlDeserializer.Deserialize<PropertySet>(new StringReader(File.ReadAllText(yamlFileName)));
                    log.Info($"--------------------------------------------------------------------------------------------------");
                    log.Info($"Opened the YAML file {yamlFileName}");
                    log.Info($"--------------------------------------------------------------------------------------------------");
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
                                    log.Info($"    Update: {existingFullName.Name}=>{localization.name}");
                                    var answer = bsdd.UpdateConceptName(pSetConcept.Guid, existingFullName.Guid, localization.language, localization.name);
                                    log.Info($"    Succesfully update => {answer.Guid}");
                                }
                                else
                                    log.Info($"    No Update needed, the names are identical: {existingFullName.Guid}");
                            }

                            var existingDefinition = pSetConcept.Definitions.Where(x => x.Language.LanguageCode.ToLower() == localization.language.ToLower()).FirstOrDefault();
                            //Dirty fix for https://github.com/buildingSMART/bSDD/issues/11
                            localization.definition = localization.definition.Replace("  ", " ");
                            if (existingDefinition == null)
                            {
                                log.Info($"    Insert description for concept => {localization.definition}");
                                var answer = bsdd.InsertConceptDefinition(pSetConcept.Guid, localization.language, localization.definition);
                                log.Info($"    Succesfully inserted => {answer.Guid}");
                            }
                            else
                            {
                                log.Info($"    Existing Translation for description {existingDefinition.Language.LanguageCode}");
                                log.Info($"    DescriptionType : {existingDefinition.DescriptionType}");
                                if (existingDefinition.Description != localization.definition)
                                { 
                                    log.Info($"    Update: {existingDefinition.Description}=>{localization.definition}");
                                    var answer = bsdd.UpdateConceptDefinition(pSetConcept.Guid, existingDefinition.Guid, localization.language, localization.definition);
                                    log.Info($"    Succesfully update => {answer.Guid}");
                                }
                                else
                                    log.Info($"    No Update needed, the definitions are identical: {existingDefinition.Guid}");
                            }
                        }

                        //Now traversing the properties of the PSet
                        foreach (var property in PSet.properties)
                        {
                            IfdConcept propertyConcept = bsdd.GetConcept(property.dictionaryReference.ifdGuid);
                            if (propertyConcept != null)
                            {
                                log.Info($"Ok, the property {property.name} lives here: {bsddUrl}/#concept/browse/{propertyConcept.Guid}");
                                log.Info($"Status: {propertyConcept.Status}");

                                foreach (var localization in property.localizations
                                                                .Where(x => x.language.ToLower() == languageCode.ToLower())
                                                                .Where(x => x.name.Length > 0))
                                {
                                    log.Info($"Publishing the Property for language {localization.language}");
                                 
                                    {   //managing the names of the property

                                        //Dirty fix for https://github.com/buildingSMART/bSDD/issues/11
                                        localization.name = localization.name.Replace("  ", " ");

                                        var existingNames = propertyConcept.FullNames.Where(x => x.Language.LanguageCode.ToLower() == localization.language.ToLower()).ToList();                               
                                        if (existingNames == null)
                                        {
                                            log.Info($"    No name exists for the language {localization.language}");
                                            log.Info($"    Insert this name as the first name : {localization.name}");
                                            var answer = bsdd.InsertConceptName(propertyConcept.Guid, localization.language, localization.name);
                                            log.Info($"    Succesfully inserted first name with GUID {answer.Guid} for the concept {propertyConcept.Guid}");
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
                                                log.Info($"    The name '{localization.name}' allready exists with the GUID {guidOfExistingName}, no insertion needed");                                          
                                            }
                                            else
                                            {
                                                log.Info($"    The name '{localization.name}' does not exist, inserting it now...");  
                                                var answer = bsdd.InsertConceptName(propertyConcept.Guid, localization.language, localization.name);
                                                guidOfNewName = answer.Guid;
                                                log.Info($"    Succesfully inserted with the GUID {guidOfNewName}");
                                            }

                                            foreach (var name in existingNames.Where(x=>x.Guid!= guidOfExistingName).Where(y=>y.Guid!= guidOfNewName))
                                            {
                                                log.Info($"    Deleting old name {name.Name} with GUID {name.Guid}");
                                                var answer = bsdd.DeleteConceptName(propertyConcept.Guid, name.Guid);
                                            }
                                        }
                                    }

                                    {   //Managing the definitions of the property

                                        //Dirty fix for https://github.com/buildingSMART/bSDD/issues/11
                                        localization.definition = localization.definition.Replace("  ", " ");

                                        var existingDefinitions = propertyConcept.Definitions.Where(x => x.Language.LanguageCode.ToLower() == localization.language.ToLower()).ToList();
                                        if (existingDefinitions == null)
                                        {
                                            log.Info($"    No description exists for the language {localization.language}");
                                            log.Info($"    Insert this description as the first name : {localization.definition}");
                                            var answer = bsdd.InsertConceptName(propertyConcept.Guid, localization.language, localization.definition);
                                            log.Info($"    Succesfully inserted first description with GUID {answer.Guid} for the concept {propertyConcept.Guid}");
                                        }
                                        else
                                        {
                                            log.Info($"    There exists {existingDefinitions.Count()} description(s) for the concept {propertyConcept.Guid} in the language {localization.language}");

                                            string guidOfExistingDefinition = null;
                                            string guidOfNewDefinition = null;
                                            if (existingDefinitions.Select(x => x.Description).ToList().Contains(localization.definition))
                                            {
                                                guidOfExistingDefinition = existingDefinitions.Where(x => x.Description == localization.definition).FirstOrDefault().Guid;
                                                log.Info($"    The description '{localization.definition}' allready exists with the GUID {guidOfExistingDefinition}, no insertion needed");
                                            }
                                            else
                                            {
                                                log.Info($"    The description '{localization.definition}' does not exist, inserting it now...");
                                                var answer = bsdd.InsertConceptName(propertyConcept.Guid, localization.language, localization.definition);
                                                guidOfNewDefinition = answer.Guid;
                                                log.Info($"    Succesfully inserted with the GUID {guidOfNewDefinition}");
                                            }

                                            foreach (var name in existingDefinitions.Where(x => x.Guid != guidOfExistingDefinition).Where(y => y.Guid != guidOfNewDefinition))
                                            {
                                                log.Info($"    Deleting old description '{name.Description}' with GUID {name.Guid}");
                                                var answer = bsdd.DeleteConceptName(propertyConcept.Guid, name.Guid);
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        log.Info($"    Update the status of the concept to APPROVED");
                        bsdd.UpdateConceptStatus(pSetConcept.Guid, IfdStatusEnum.APPROVED);
                        log.Info($"    Succesfully updated");
                    }
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