using System.Collections.Generic;

namespace bSDD.DemoClientConsole.Contract
{
    public class SearchResultContract
    {
        /// <summary>
        /// The total number of Classifications matching the search criteria
        /// </summary>
        public int NumberOfClassificationsFound { get; set; }

        /// <summary>
        /// The list of Domains with found Classification and ClassificationProperties
        /// </summary>
        public List<DomainSearchResultContract> Domains { get; set; }
    }
}
