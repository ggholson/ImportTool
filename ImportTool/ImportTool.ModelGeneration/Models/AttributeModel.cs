namespace ImportTool.ModelGeneration.Models
{
    using System.CodeDom;
    using System.Linq;

    public class AttributeModel
    {
        public AttributeModel(string name)
            : this(name, null)
        {            
        }

        public AttributeModel(string name, object[] args)
        {
            this.Name = name;
            this.Arguments = args;
        }

        public string Name { get; private set; }

        public object[] Arguments { get; set; }

        public CodeAttributeArgument[] GetArguments()
        {
            return Arguments.Select(a => new CodeAttributeArgument(new CodePrimitiveExpression(a))).ToArray();
        }
    }
}
