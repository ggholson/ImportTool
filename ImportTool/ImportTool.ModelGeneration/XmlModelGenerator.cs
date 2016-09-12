namespace ImportTool.ModelGeneration
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;

    using ImportTool.ModelGeneration.Models;
    using ImportTool.ModelGeneration.Test;

    public class XmlModelGenerator
    {
        private XDocument xmlDoc;

        public XmlModelGenerator(string xmlFilePath)
        {
            this.xmlDoc = XDocument.Load(xmlFilePath);
        }

        public IList<XmlContainerModel> GetUniqueContainers()
        {
            return this.GetUniqueContainers(this.xmlDoc.Root, 0);
        }

        /// <summary>
        /// Method which recursively returns all uniquely named child 
        /// container nodes that are its children. 
        /// </summary>
        public IList<XmlContainerModel> GetUniqueContainers(XContainer root, int rootNodeLevel)
        {
            List<XmlContainerModel> uniqueNodes = new List<XmlContainerModel>();

            IEnumerable<XElement> levelOneNodes = this.getDistinctNodesFromContainer(root);

            foreach (var node in levelOneNodes)
            {
                if (this.isElementContainer(node))
                {
                    // If we made it here, the current node IS a child container.
                    // Add it to the list and start parsing it's children.
                    uniqueNodes.Add(new XmlContainerModel(node, rootNodeLevel + 1));
                    IList<XmlContainerModel> childContainers = this.GetUniqueContainers(node, rootNodeLevel + 1);

                    if (childContainers != null)
                    {
                        foreach (var childContainer in childContainers)
                        {
                            // We only want to add the results from the child nodes if they are not already in the list
                            if (!uniqueNodes.Contains(childContainer, new XmlElementNameComparer()))
                            {
                                uniqueNodes.Add(childContainer);
                            }
                        }
                    }
                }
            }

            return uniqueNodes;
        }

        private IEnumerable<XElement> getDistinctNodesFromContainer(XContainer container)
        {
            IEnumerable<XElement> nodes = container.Elements();
            IEnumerable<XElement> uniqueNodes = nodes.Distinct(new XmlElementNameComparer());
                //nodes.GroupBy(n => n.Name.LocalName).Select(n => n.First());
            return uniqueNodes;
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
