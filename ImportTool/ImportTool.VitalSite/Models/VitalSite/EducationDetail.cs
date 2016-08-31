namespace ImportTool.VitalSite.Models.VitalSite
{
    using System;

    public class EducationDetail
    {
        public string Institution { get; set; }
        public string Program { get; set; }
        public string StartYear { get; set; }
        public string EndYear { get; set; }

        public EducationDetail(string institution)
            : this(institution, null)
        {
        }

        public EducationDetail(string institution, string program)
            : this(institution, program, null)
        {
        }

        public EducationDetail(string institution, string program, string startYear)
            : this(institution, program, startYear, null)
        {
        }

        public EducationDetail(string institution, string program, string startYear, string endYear)
        {
            this.Institution = institution;
            this.Program = program;
            this.StartYear = startYear;
            this.EndYear = endYear;
        }

        public static EducationDetail Parse(string s)
        {
            if (string.IsNullOrWhiteSpace(s)) return null;

            string[] tokens = s.Split("(".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

            if (tokens.Length == 2)
            {
                return new EducationDetail(tokens[0], tokens[1].TrimEnd(")".ToCharArray()));
            }

            return new EducationDetail(tokens[0]);
        }
    }
}
