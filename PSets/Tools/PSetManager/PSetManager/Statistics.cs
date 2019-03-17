using PSets4;
using PSets5;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.Serialization;

namespace PSetManager
{
    public class Statistics
    {

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public Statistics(string folderYaml)
        {
            var yamlFileNames = Directory.EnumerateFiles(folderYaml, "PSet*.YAML");//.Where(x => x.Contains("Pset_StairCommon"));

            List<PSetStatistics> stats = new List<PSetStatistics>();

            foreach (string yamlFileName in yamlFileNames)
            {

                var yamlDeserializer = new DeserializerBuilder().Build();
                PropertySet PSet = yamlDeserializer.Deserialize<PropertySet>(new StringReader(File.ReadAllText(yamlFileName)));

                foreach (var property in PSet.properties)
                {
                    stats.Add(new PSetStatistics
                    {
                        pset = PSet.name,
                        property = property.name,
                        definition = property.definition
                    });
                }
            }

            foreach (var item in stats.OrderBy(x => x.property).ThenBy(x => x.pset))
            log.Info($"{item.property}|{item.pset}|{item.definition}");
        }
    }
}
