using System;
using System.Collections.Generic;

namespace bSDD.DemoClientConsole.Contract
{
    public class ClassificationSearchResultContract
    {
        public Guid Guid { get; set; }
        public string Name { get; set; }
        public string Definition { get; set; }

        public List<string> Synonyms { get; set; }
    }
}
