namespace ImportTool.VitalSite.Models.VitalSite
{
    using System;
    using System.Collections.Generic;

    public class TaxonomyFacet
    {
        private string facetName;
        private List<string> terms;

        public TaxonomyFacet(string facetName)
        {
            if (string.IsNullOrWhiteSpace(facetName))
            {
                throw new ArgumentNullException("facetName");
            }

            this.facetName = facetName;
            this.terms = new List<string>();
        }

        public string FacetName
        {
            get { return this.facetName; }
        }

        public List<string> Terms
        {
            get { return this.terms; }
        }
    }
}
