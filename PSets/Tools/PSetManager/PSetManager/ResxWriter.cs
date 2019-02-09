using PSets5;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Resources;


public class ResxWriter
{
    private string _fileName;
    public ResxWriter(string fileName)
    {
        _fileName = fileName;
    }

    public void Write(PropertySet propertySet, List<string> standardLanguages)
    {
        using (ResXResourceWriter resx = new ResXResourceWriter(_fileName))
        {
            resx.AddResource($"PSet.Name[{propertySet.dictionaryReference.ifdGuid}]", propertySet.name);
            resx.AddResource($"PSet.Definition[{propertySet.dictionaryReference.ifdGuid}]", propertySet.definition);

            foreach (Property property in propertySet.properties)
            {
                resx.AddResource($"Property.{property.name}.Name[{property.dictionaryReference.ifdGuid}]", property.name);
                resx.AddResource($"Property.{property.name}.Definition[{property.dictionaryReference.ifdGuid}]", property.definition);
            }
        }

        foreach (string lang in standardLanguages)
        {
            string languageSpecificFileName = _fileName.Replace("resx", $"{lang}.resx");
            using (ResXResourceWriter resx = new ResXResourceWriter(languageSpecificFileName))
            {
                Localization localizationInSpecificLanguage = propertySet.localizations.Where(x => x.language == lang).FirstOrDefault();
                resx.AddResource($"PSet.Name[{propertySet.dictionaryReference.ifdGuid}]", localizationInSpecificLanguage.name ?? string.Empty);
                resx.AddResource($"PSet.Definition[{propertySet.dictionaryReference.ifdGuid}]", localizationInSpecificLanguage.definition ?? string.Empty);

                foreach (Property property in propertySet.properties)
                {
                    localizationInSpecificLanguage = property.localizations.Where(l=> l.language == lang).FirstOrDefault();

                    resx.AddResource($"Property.{property.name}.Name[{property.dictionaryReference.ifdGuid}]", localizationInSpecificLanguage.name ?? string.Empty);
                    resx.AddResource($"Property.{property.name}.Definition[{property.dictionaryReference.ifdGuid}]", localizationInSpecificLanguage.definition ?? string.Empty);
                }
            }

        }
    }
}