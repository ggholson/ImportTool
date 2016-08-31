namespace ImportTool.VitalSite.Models.VitalSite
{
    using System;
    using System.Collections.Generic;

    public class EducationBackground
    {
        private string type;
        private List<EducationDetail> details;

        public EducationBackground(string type)
        {
            if (string.IsNullOrWhiteSpace(type))
            {
                throw new ArgumentNullException("type");
            }

            this.type = type;
            this.details = new List<EducationDetail>();
        }

        public string EducationType
        {
            get { return this.type; }
        }

        public List<EducationDetail> Details
        {
            get { return this.details; }
        }
    }
}
