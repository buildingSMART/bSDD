using System;
using System.Collections.Generic;
using CommandLine;

namespace PSets4
{
    class CommandLineOptions
    {
        [Option('m', "mode", Required = true, HelpText = "The working mode of the PSet Manager. Avalilable modes are 'ConvertFromXml','LoadTranslation','PublishToBSDD'")]
        public string mode { get; set; }

        [Option('x', "folderXml", Required = false, HelpText = "Path of the folder, that contains the serialization of the PSets in the XML schema of IFC 4")]
        public string folderXml { get; set; }

        [Option('y', "folderYaml", Required = false, HelpText = "Path of the folder, that contains the serialization PSets in the YAML schema")]
        public string folderYaml { get; set; }

        [Option('j', "folderJson", Required = false, HelpText = "Path of the folder, that contains the serialization of the PSets in the JSON schema")]
        public string folderJson { get; set; }

        [Option('r', "folderResx", Required = false, HelpText = "Path of the folder, that contains the translation of the serialization of the PSets in the RESX schema")]
        public string folderResx { get; set; }

        [Option('t', "translationSourceFile", Required = false, HelpText = "Path to an Excel sheet in the format XLSX (based on the translation template), load the file and inject the translations into the YAML files")]
        public string translationSourceFile { get; set; }

        [Option('b', "checkBSDD", Required = false, Default = true, HelpText = "Check the connection to the bSDD (network required!)")]
        public bool checkBSDD { get; set; }

        [Option('b', "bsddUrl", Required = false, HelpText = "URL of the bSDD ('http://test.bsdd.buildingsmart.org' or 'http://bsdd.buildingsmart.org'))")]
        public string bsddUrl { get; set; }

        [Option('u', "bsddUser", Required = false, HelpText = "User for the bSDD")]
        public string bsddUser { get; set; }

        [Option('p', "bsddPassword", Required = false, HelpText = "Password for the bsDD")]
        public string bsddPassword { get; set; }

        [Option('l', "bsddLanguageCode", Required = false, HelpText = "Language, that shall be published to the bsDD")]
        public string bsddLanguageCode { get; set; }

    }
}
