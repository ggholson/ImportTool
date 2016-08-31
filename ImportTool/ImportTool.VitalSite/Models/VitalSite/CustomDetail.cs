namespace ImportTool.VitalSite.Models.VitalSite
{
    using System;
    using System.Collections.Generic;

    public class CustomDetail
    {
        private readonly string fieldName;
        private List<string> details;
        private CustomDetailContentStyle style;

        public CustomDetail(string fieldName)
            : this(fieldName, CustomDetailContentStyle.OnePerLine)
        {
        }

        public CustomDetail(string fieldName, CustomDetailContentStyle style)
        {
            if (string.IsNullOrWhiteSpace(fieldName))
            {
                throw new ArgumentNullException("fieldName");
            }

            this.fieldName = fieldName;
            this.details = new List<string>();
            this.style = style;
        }

        public string FieldName
        {
            get { return this.fieldName; }
        }

        public List<string> Details
        {
            get { return this.details; }
        }

        public CustomDetailContentStyle Style
        {
            get { return this.style; }
        }

        public string GetContent(CustomDetailContentStyle style)
        {
            string content = string.Empty;

            foreach (var detail in this.details)
            {
                if (style.Equals(CustomDetailContentStyle.BulletedList))
                {
                    content += "-";
                }
                content += detail;

                switch (style)
                {
                    case CustomDetailContentStyle.OnePerLine:
                    case CustomDetailContentStyle.BulletedList:
                        content += "\r\n";
                        break;
                    case CustomDetailContentStyle.CommaSeparated:
                        content += ", ";
                        break;
                }
            }

            return content;
        }
    }
}
