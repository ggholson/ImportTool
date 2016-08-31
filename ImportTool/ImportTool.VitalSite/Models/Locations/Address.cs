namespace ImportTool.VitalSite.Models.Locations
{
    using ImportTool.Core.Contracts;

    public class Address : IImportModel
    {
        public string FacilityName { get; private set; }
        public string StreetLine1 { get; private set; }
        public string StreetLine2 { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public string Zip { get; private set; }

        public Address(string facility, string line1, string line2, string city,
            string state, string zip)
        {
            this.FacilityName = facility;

            if (line1 != null && line1.Length > 64 && line1.IndexOf("/group") > 0)
            {
                // HACK
                this.StreetLine1 = line1.Substring(0, line1.IndexOf("/group"));
            }
            else
            {
                this.StreetLine1 = line1;
            }

            this.StreetLine2 = line2;
            this.City = city;
            this.State = state;
            this.Zip = zip;
        }

        public bool IsValid()
        {
            if (string.IsNullOrWhiteSpace(this.StreetLine1) ||
                string.IsNullOrWhiteSpace(this.City) ||
                string.IsNullOrWhiteSpace(this.State) ||
                string.IsNullOrWhiteSpace(this.Zip))
                return false;

            return true;
        }
    }
}
