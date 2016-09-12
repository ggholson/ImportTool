namespace ImportTool.ModelGeneration.Models
{
    using System.Collections.Generic;
    using System.Xml.Linq;

    public class XmlContainerModel
    {
        public XmlContainerModel(XElement element, int nodeLevel)
        {
            this.Element = element;
            this.NodeLevel = nodeLevel;
        }

        public string Name => this.Element.Name.LocalName;

        public XElement Element { get; set; }

        public int NodeLevel { get; set; }
    }
}
