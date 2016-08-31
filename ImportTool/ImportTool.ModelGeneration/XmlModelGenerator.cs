namespace ImportTool.ModelGeneration
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Xml;

    public class XmlModelGenerator
    {
        private XmlDocument xml;

        public XmlModelGenerator(string xmlFilePath)
        {
            this.xml = new XmlDocument();
            this.xml.Load(xmlFilePath);
        }

        public void GenerateModel()
        {
            XmlNode model = this.xml.DocumentElement.FirstChild;
        }

        private 
    }
}
