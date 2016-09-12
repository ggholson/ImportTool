namespace ImportTool.ModelGeneration.Test
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Xml.Linq;

    using ImportTool.ModelGeneration.Models;

    using NUnit.Framework;

    [TestFixture]
    public class XmlModelGeneratorTests
    {
        private string xmlFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Files", "xmlTestDocument.xml");
        private readonly string xmlFilePath2 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Files", "XmlTestMultiRoot.xml");
        private readonly string realXmlFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Files", "test-upload-echo-bmg.xml");
        private string csFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Files", "XmlTestDocumentModel.cs");

        [Test]
        public void CanGenerateModelFromFile()
        {
            XmlModelGenerator gen = new XmlModelGenerator(this.xmlFilePath2);
            IList<XmlContainerModel> uniqueNodes = gen.GetUniqueContainers();

            List<string> nodeNames = uniqueNodes.Select(n => n.Name).ToList();

            Assert.AreEqual(nodeNames.Count, 3);
            Assert.IsTrue(nodeNames.Contains("ChildNode"));
            Assert.IsTrue(nodeNames.Contains("ChildContainer"));
            Assert.IsTrue(nodeNames.Contains("SecondaryChildNode"));
        }

        [Test]
        public void CanGenerateModelFromActualFile()
        {
            XmlModelGenerator gen = new XmlModelGenerator(this.realXmlFilePath);
            IList<XmlContainerModel> uniqueNodes = gen.GetUniqueContainers();

            List<string> nodeNames = uniqueNodes.Select(n => n.Name).ToList();

//            Assert.AreEqual(nodeNames.Count, 3);
//            Assert.IsTrue(nodeNames.Contains("ChildNode"));
//            Assert.IsTrue(nodeNames.Contains("ChildContainer"));
//            Assert.IsTrue(nodeNames.Contains("SecondaryChildNode"));
        }
    }
}
