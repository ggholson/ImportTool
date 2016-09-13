namespace ImportTool.VitalSite.Models.VitalSite
{
    using System;
    using System.Collections.Generic;

    public class TaxonomyFacet
    {
        public TaxonomyFacet(string facetName)
        {
            if (string.IsNullOrWhiteSpace(facetName))
            {
                throw new ArgumentNullException(nameof(facetName));
            }

            this.FacetName = facetName;
            this.Terms = new List<string>();
        }

        public string FacetName { get; }

        public List<string> Terms { get; }
    }
}
