namespace ImportTool.ModelGeneration.Test
{
    using System;
    using System.IO;

    using NUnit.Framework;

    [TestFixture]
    public class XmlModelGeneratorTests
    {
        private string xmlFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Files", "xmlTestDocument.xml");
        private string xmlFilePath2 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Files", "XmlTestMultiRoot.xml");
        private string csFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Files", "XmlTestDocumentModel.cs");

        [Test]
        public void CanGenerateModelFromFile()
        {
            XmlModelGenerator gen = new XmlModelGenerator(this.xmlFilePath2);
            gen.GetUniqueNodes();
        }
    }
}
