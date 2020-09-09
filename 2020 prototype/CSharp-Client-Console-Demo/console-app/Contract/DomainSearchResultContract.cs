using System;
using System.Collections.Generic;

namespace bSDD.DemoClientConsole.Contract
{
    public class DomainSearchResultContract
    {
        public Guid Guid { get; set; }
        public string Name { get; set; }

        public List<ClassificationSearchResultContract> Classifications { get; set; }
    }
}
