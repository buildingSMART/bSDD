using System;
using YamlDotNet.Serialization;
using YamlDotNet.Helpers;
using System.Text.RegularExpressions;
using YamlDotNet.Serialization.EventEmitters;
using YamlDotNet.Core;

namespace PSet2YamlConverter
{
    public static class Utils
    {
        public static string CleanUp(string str)
        {
            str = Quote(Regex.Replace(str, @"\r\n?|\n", string.Empty));
            str = Quote(Regex.Replace(str, @"\n", string.Empty));
            str = str.Trim();

            return str;
        }
        public static string Quote(string text)
        {
            string doubleQuote = string.Empty + (char)34;
            if (text.Substring(0) == doubleQuote)
                text = text.Remove(1, text.Length);

            return text;
        }
        public static string FirstUpperRestLower(string str)
        {
            str = CleanUp(str);
            return Utils.CleanUp(str.ToString().Substring(0, 1) + str.ToString().Substring(1, str.ToString().Length - 1).ToLower());
        }
        public static string GuidConverterToIfcGuid(string guidInStringFormat)
        {

            if (guidInStringFormat is null) return string.Empty;
            if (guidInStringFormat.Length == 0) return string.Empty;
            //Check, if the guid is in the PSet format: ce28c7bdc6e445d7893623a590ddda99
            //https://github.com/buildingSMART/IfcDoc/issues/3


            if (!guidInStringFormat.Contains("-"))
            {
                //Convert into the format "8-4-4-4-12"
                //ce28c7bd-c6e4-45d7-8936-23a590ddda99
                guidInStringFormat = guidInStringFormat.Substring(0, 8) + "-" + guidInStringFormat.Substring(8, 4) + "-" + guidInStringFormat.Substring(12, 4) + "-" + guidInStringFormat.Substring(16, 4) + "-" + guidInStringFormat.Substring(20, 12);           
            }

            Guid guid;
            try
            {
                guid = new Guid(guidInStringFormat);
            }
            catch
            {
                return $"Guid {guidInStringFormat} is not valid. Tranformation into the 8-4-4-4-12 format has failed.";
            }

            
            //Convert GUID zu IFC.GlobalId
            //https://github.com/buildingSMART/GuidConverter

            try
            {
                return GuidConverter.ConvertToIfcGuid(guid);
            }

            catch
            {
                return $"Guid {guidInStringFormat} is not valid. Tranformation into the IFC format has failed.";
            }

        }
        public static PSD_IFC5.PropertySet PrepareTexts(PSD_IFC5.PropertySet propertySet)
        {
            propertySet.definition = Utils.CleanUp(propertySet.definition);
            foreach (var prop in propertySet.properties)
            {
                prop.definition = Utils.CleanUp(prop.definition);
                foreach (var lang in prop.localizations)
                {
                    lang.definition = Utils.CleanUp(lang.definition);
                }
            }

            return propertySet;
        }
    }
}