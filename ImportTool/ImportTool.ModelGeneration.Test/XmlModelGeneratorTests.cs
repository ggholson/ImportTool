namespace ImportTool.ModelGeneration.Test
{
    using System;

    using NUnit.Framework;

    [TestFixture]
    public class XmlModelGeneratorTests
    {
        private string xmlFilePath = $"{AppDomain.CurrentDomain.BaseDirectory}\\Files\\xmlTestDocument.xml";
        private string csFilePath = $"{AppDomain.CurrentDomain.BaseDirectory}\\Files\\XmlTestDocumentModel.cs";

        [Test]
        public void CanGenerateModelFromFile()
        {
            XmlModelGenerator gen = new XmlModelGenerator(this.xmlFilePath);
            gen.GenerateModel();
        }
    }
}
