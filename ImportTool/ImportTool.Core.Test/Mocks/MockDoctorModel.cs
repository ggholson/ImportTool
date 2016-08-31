namespace ImportTool.Core.Test.Mocks
{
    using System.Collections.Generic;
    using System.Xml.Serialization;

    public class MockDoctorModel
    {
        public int Id { get; set; }
    }

    [XmlRoot("Doctors")]
    public class MockDoctorModelList
    {
        [XmlElement("Doctor")]
        public List<MockDoctorModel> Doctors { get; set; }
    }
}
