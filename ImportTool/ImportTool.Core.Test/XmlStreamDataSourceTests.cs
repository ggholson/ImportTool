namespace ImportTool.Core.Test
{
    using System;
    using System.IO;

    using ImportTool.Core.DataAccess;
    using ImportTool.Core.Test.Mocks;

    using NUnit.Framework;

    [TestFixture]
    public class XmlStreamDataSourceTests{

        private string testFilePath = $"{AppDomain.CurrentDomain.BaseDirectory}\\Files\\xmlTestDocument.xml";

        [Test]
        public void CanOpenAndCloseFile()
        {
            Assert.DoesNotThrow(() =>
            {
                XmlStreamDataSource reader = new XmlStreamDataSource(this.testFilePath);
                reader.Initialize();
                reader.Dispose();
            });
        }

        public void CanReadAllElementsFromFile()
        {
            XmlStreamDataSource reader = new XmlStreamDataSource(this.testFilePath);
            reader.Initialize();
            MockDoctorModel model = reader.ReadNext<MockDoctorModel>();
        }
    }
}
