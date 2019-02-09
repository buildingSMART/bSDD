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

                                    var existingName = propertyConcept.FullNames.Where(x => x.Language.LanguageCode.ToLower() == localization.language.ToLower()).FirstOrDefault();
                                    //Dirty fix for https://github.com/buildingSMART/bSDD/issues/11
                                    localization.name = localization.name.Replace("  ", " ");
                                    if (existingName == null)
                                    {
                                        log.Info($"    Insert Name for concept => {localization.name}");
                                        var answer = bsdd.InsertConceptName(propertyConcept.Guid, localization.language, localization.name);
                                        log.Info($"    Succesfully inserted => {answer.Guid}");
                                    }
                                    else
                                    {
                                        log.Info($"    Existing Translation for name {existingName.Language.LanguageCode}");
                                        log.Info($"    NameType : {existingName.NameType}");
                                        if (existingName.Name != localization.name)
                                        {
                                            log.Info($"    Update: {existingName.Name}=>{localization.name}");
                                            var answer = bsdd.UpdateConceptName(propertyConcept.Guid, existingName.Guid, localization.language, localization.name);
                                            log.Info($"    Succesfully update => {answer.Guid}");
                                        }
                                        else
                                            log.Info($"    No Update needed, the names are identical: {existingName.Guid}");
                                    }

                                    var existingDefinition = propertyConcept.Definitions.Where(x => x.Language.LanguageCode.ToLower() == localization.language.ToLower()).FirstOrDefault();
                                    //Dirty fix for https://github.com/buildingSMART/bSDD/issues/11
                                    localization.definition = localization.definition.Replace("  ", " ");
                                    if (existingDefinition == null)
                                    {
                                        log.Info($"    Insert description for concept => {localization.definition}");
                                        var answer = bsdd.InsertConceptDefinition(propertyConcept.Guid, localization.language, localization.definition);
                                        log.Info($"    Succesfully inserted => {answer.Guid}");
                                    }
                                    else
                                    {
                                        log.Info($"    Existing Translation for description {existingDefinition.Language.LanguageCode}");
                                        log.Info($"    DescriptionType : {existingDefinition.DescriptionType}");
                                        if (existingDefinition.Description != localization.definition)
                                        {
                                            log.Info($"    Update: {existingDefinition.Description}=>{localization.definition}");
                                            var answer = bsdd.UpdateConceptDefinition(propertyConcept.Guid, existingDefinition.Guid, localization.language, localization.definition);
                                            log.Info($"    Succesfully update => {answer.Guid}");
                                        }
                                        else
                                            log.Info($"    No Update needed, the definitions are identical: {existingDefinition.Guid}");
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