namespace ImportTool.VitalSite.Models.Doctors
{
    using System.Collections.Generic;

    using ImportTool.VitalSite.Models.Locations;
    using ImportTool.VitalSite.Models.VitalSite;

    public class Physician
    {
        public string ClientId { get; private set; }

        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Suffix { get; set; }
        public Gender Gender { get; set; }

        public bool IsAcceptingNewPatients { get; set; }
        public bool IsActive { get; set; }

        public List<Location> Locations { get; private set; }

        public TaxonomyFacet Specialties { get; private set; }
        public TaxonomyFacet Subspecialties { get; private set; }
        public TaxonomyFacet Department { get; private set; }
        public TaxonomyFacet Regions { get; private set; }
        public TaxonomyFacet ProfessionalTitles { get; private set; }
        public TaxonomyFacet ProviderType { get; private set; }
        public TaxonomyFacet AreasOfInterest { get; private set; }
        public TaxonomyFacet PromotionalDesignation { get; private set; }
        public TaxonomyFacet ServiceLine { get; private set; }
        public TaxonomyFacet Affiliations { get; private set; }
        public TaxonomyFacet PatientTimeframe { get; private set; }
        public TaxonomyFacet Languages { get; private set; }
        public TaxonomyFacet SocietyMemberships { get; private set; }

        public CustomDetail BoardCertifications { get; private set; }
        public CustomDetail ClinicalInterests { get; private set; }
        public CustomDetail SpecialFocus { get; private set; }
        public CustomDetail ProfessionalMemberships { get; private set; }
        public CustomDetail TeachingOrProfessionalPositions { get; private set; }
        public CustomDetail HonorsAwards { get; private set; }

        public EducationBackground MedicalSchool { get; private set; }
        public EducationBackground Residency { get; private set; }
        public EducationBackground Fellowship { get; private set; }
        public EducationBackground Masters { get; private set; }
        public EducationBackground DentalSchool { get; private set; }
        public EducationBackground GradSchool { get; private set; }
        public EducationBackground NursingSchool { get; private set; }
        public EducationBackground Undergrad { get; private set; }
        public EducationBackground TravelingFellowship { get; private set; }
        public EducationBackground Internships { get; private set; }

        public Physician(string clientId)
        {
            this.ClientId = clientId;

            this.IsAcceptingNewPatients = false;
            this.IsActive = false;

            this.Locations = new List<Location>();

            this.Specialties = new TaxonomyFacet("Specialties");
            this.Subspecialties = new TaxonomyFacet("Subspecialties");
            this.ProfessionalTitles = new TaxonomyFacet("Professional Titles");
            this.Affiliations = new TaxonomyFacet("Affiliations");
            this.Languages = new TaxonomyFacet("Languages");
            this.PatientTimeframe = new TaxonomyFacet("Patient Timeframe");
            this.SocietyMemberships = new TaxonomyFacet("Society Memberships");
            this.Regions = new TaxonomyFacet("Regions");
            this.ProviderType = new TaxonomyFacet("Provider Type");
            this.AreasOfInterest = new TaxonomyFacet("Areas of Interest");
            this.PromotionalDesignation = new TaxonomyFacet("Promotional Designations");
            this.ServiceLine = new TaxonomyFacet("Service Line");

            this.ClinicalInterests = new CustomDetail("Clinical Interests");
            this.SpecialFocus = new CustomDetail("Special Focus");
            this.BoardCertifications = new CustomDetail("Board Certifications", CustomDetailContentStyle.BulletedList);
            this.ProfessionalMemberships = new CustomDetail("Professional Memberships", CustomDetailContentStyle.BulletedList);
            this.TeachingOrProfessionalPositions = new CustomDetail("Teaching or Professional Positions", CustomDetailContentStyle.BulletedList);
            this.HonorsAwards = new CustomDetail("Honors, Awards, etc.", CustomDetailContentStyle.BulletedList);


            this.MedicalSchool = new EducationBackground("Medical School");
            this.Residency = new EducationBackground("Residency");
            this.Fellowship = new EducationBackground("Fellowship");
            this.Masters = new EducationBackground("Masters");
            this.DentalSchool = new EducationBackground("Dental School");
            this.GradSchool = new EducationBackground("Graduate School");
            this.NursingSchool = new EducationBackground("Nursing School");
            this.Undergrad = new EducationBackground("Undergraduate");
            this.TravelingFellowship = new EducationBackground("Traveling Fellowship");
            this.Internships = new EducationBackground("Internships");
        }
    }
}
