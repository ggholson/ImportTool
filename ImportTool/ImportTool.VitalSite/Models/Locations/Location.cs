namespace ImportTool.VitalSite.Models.Locations
{
    using ImportTool.Core.Contracts;

    public class Location : IImportModel
    {
        public string DisplayName { get; private set; }

        public Address Address { get; private set; }

        public string PhoneNumber { get; set; }

        public string FaxNumber { get; set; }

        public string Website { get; set; }

        public bool DisplayWebsite { get; set; }

        public bool IsPrimary { get; set; }

        public Location(string name, string facility, string line1, string line2, string city, string state, string zip)
            : this(name, new Address(facility, line1, line2, city, state, zip))
        {
        }

        public Location(string name, Address address)
        {
            if (name != null)
                this.DisplayName = name.Length > 100 ? name.Substring(0, 100) : name;
            this.Address = address;
        }

        public bool IsValid()
        {
            if (!string.IsNullOrWhiteSpace(this.DisplayName) &&
                this.Address.IsValid()) return true;

            return false;
        }
    }
}
