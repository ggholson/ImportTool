using System;
using System.Linq;

namespace ImportTool.ModelGeneration
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Xml.Linq;

    public class XmlModelGenerator
    {
        private XDocument xmlDoc;

        public XmlModelGenerator(string xmlFilePath)
        {
            this.xmlDoc = XDocument.Load(xmlFilePath);
        }

        public IList<XContainer> GetUniqueNodes()
        {
            return this.GetUniqueNodes(this.xmlDoc.Root);
        }

        public IList<XContainer> GetUniqueNodes(XContainer root)
        {
            List<XContainer> uniqueNodes = new List<XContainer>();

            IEnumerable<XElement> levelOneNodes = this.getDistinctNodesFromContainer(root);

            foreach (var node in levelOneNodes)
            {
                if (this.isElementContainer(node))
                {
                    uniqueNodes.Add(node);

                    XContainer container = node;
                    foreach (var child in container.Elements())
                    {
                        uniqueNodes.AddRange(this.GetUniqueNodes(child));
                    }
                }
            }

            return uniqueNodes;
        }

        private IEnumerable<XElement> getDistinctNodesFromContainer(XContainer container)
        {
            IEnumerable<XElement> levelOneNodes = container.Elements();
            IEnumerable<XElement> uniqueLevelOneNodes =
                levelOneNodes.GroupBy(n => n.Name.LocalName).Select(n => n.First());
            return uniqueLevelOneNodes;
        }

        private bool isElementContainer(XElement element)
        {
            return !element.HasAttributes && !isTextNode(element) && element.HasElements;
        }

        private bool isTextNode(XElement element)
        {
            return element.Descendants().OfType<XText>().Any();
        }
    }
}
