namespace ImportTool.ModelGeneration
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Xml;
    using System.Xml.Linq;

    public class XmlModelGenerator
    {
        private XDocument xmlDoc;

        public XmlModelGenerator(string xmlFilePath)
        {
            this.xmlDoc = XDocument.Load(xmlFilePath);
        }

        public void GenerateModel()
        {
            var uniqueNodes = this.xmlDoc.Root.Elements();
        }
    }
}
