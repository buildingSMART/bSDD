using bSDD.NET.Model.Objects;
using System;
using System.Collections.Generic;
using System.Text;

namespace bSDD.NET
{
    class NewNameDto
    {
        /// <summary>
        /// the GUID of the language of the name
        /// </summary>
        public string languageGuid { get; set; }

        /// <summary>
        /// the name itself
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// the name type
        /// </summary>
        public IfdNameTypeEnum nameType { get; set; }
    }
}
