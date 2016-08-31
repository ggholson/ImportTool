namespace ImportTool.ModelGeneration.Models
{
    public static class NamespaceReference
    {
        public static NamespaceAssemblyReference System => new NamespaceAssemblyReference { Namespace = "System", ContainingAssembly = "System.dll" };
        public static NamespaceAssemblyReference SystemXml => new NamespaceAssemblyReference { Namespace = "System.Xml", ContainingAssembly = "System.Xml.dll" };
        public static NamespaceAssemblyReference SystemXmlSerialization => new NamespaceAssemblyReference { Namespace = "System.Xml.Serialization", ContainingAssembly = "System.Xml.dll" };
    }
}
