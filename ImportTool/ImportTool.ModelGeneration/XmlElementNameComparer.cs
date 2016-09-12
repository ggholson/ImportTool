namespace ImportTool.ModelGeneration.Test
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Xml.Linq;

    using ImportTool.ModelGeneration.Models;

    public class XmlElementNameComparer : IEqualityComparer<XElement>, IEqualityComparer<XmlContainerModel>
    {
        public bool Equals(XElement x, XElement y)
        {
            return x.Name.LocalName.Equals(y.Name.LocalName);
        }

        public int GetHashCode(XElement obj)
        {
            return obj.Name.LocalName.GetHashCode();
        }

        public bool Equals(XmlContainerModel x, XmlContainerModel y)
        {
            return x.Name.Equals(y.Name);
        }

        public int GetHashCode(XmlContainerModel obj)
        {
            return obj.Name.GetHashCode();
        }
    }
}
