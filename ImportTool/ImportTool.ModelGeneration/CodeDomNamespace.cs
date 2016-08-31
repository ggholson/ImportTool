namespace ImportTool.ModelGeneration
{
    using System.CodeDom;
    using System.Collections.Generic;
    using System.Linq;

    using ImportTool.ModelGeneration.Models;

    public class CodeDomNamespace
    {
        internal CodeDomNamespace(string name)
        {
            this.Name = name;
            this.References = new List<NamespaceAssemblyReference>();
        }

        public string Name { get; }

        public IList<NamespaceAssemblyReference> References { get; }

        public CodeNamespace ConvertToCodeNamespace()
        {
            var obj = new CodeNamespace(this.Name);
            obj.Imports.AddRange(this.References.Select(r => new CodeNamespaceImport(r.Namespace)).ToArray());
            return obj;
        }

        public IList<CodeNamespaceImport> GetImports()
        {
            return References.Select(r => new CodeNamespaceImport(r.Namespace)).ToList();
        }

        public string[] GetAssemblyReferences()
        {
            return this.References.Select(r => r.ContainingAssembly).ToArray();
        }
    }
}
