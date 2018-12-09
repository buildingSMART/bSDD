using System;
using System.Collections.Generic;
using CommandLine;

namespace PSetManager
{
    class CommandLineOptions
    {
        [Option('m', "mode", Required = true, HelpText = "The working mode of the PSet Manager. Avalilable modes are 'ConvertFromXml','ConvertFromYAML'")]
        public string mode { get; set; }

        [Option('x', "folderXml", Required = true, HelpText = "Path of the folder, that contains the serialization of the PSets in the XML schema of IFC 4")]
        public string folderXml { get; set; }

        [Option('y', "folderYaml", Required = false, HelpText = "Path of the folder, that contains the serialization PSets in the YAML schema")]
        public string folderYaml { get; set; }

        [Option('j', "folderJson", Required = false, HelpText = "Path of the folder, that contains the serialization of the PSets in the JSON schema")]
        public string folderJson { get; set; }

        [Option('j', "folderResx", Required = false, HelpText = "Path of the folder, that contains the translation of the serialization of the PSets in the RESX schema")]
        public string folderResx { get; set; }

        [Option('b', "checkBSDD", Required = false, Default = true, HelpText = "Check the connection to the bSDD (network required!)")]
        public bool checkBSDD { get; set; }
    }
}
