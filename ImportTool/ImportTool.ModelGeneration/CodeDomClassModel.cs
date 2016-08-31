namespace ImportTool.ModelGeneration
{
    using System;
    using System.CodeDom;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;

    using ImportTool.ModelGeneration.Models;

    using Microsoft.CSharp;

    /// <summary>
    /// Class which provides a layer of abstraction on top of CodeDom that
    /// assists in creation of POCO models and their compilation.
    /// </summary>
    public class CodeDomClassModel
    {
        private readonly string filePath;
        private bool isReadyToCompile = false;

        private readonly CodeTypeDeclaration targetClass;

        /// <summary>
        /// Internal constructor for instantiating a new POCO class with the
        /// specified name and file system location. Note: Class should usually
        /// be instantaited from <see cref="CodeDomAssemblyCoordinator"/>
        /// </summary>
        internal CodeDomClassModel(string outputFilePath, string outputClassName)
        {
            this.filePath = outputFilePath;
            this.ClassName = outputClassName;

            this.targetClass = new CodeTypeDeclaration(outputClassName);

            this.targetClass.IsClass = true;
            this.targetClass.TypeAttributes =
                TypeAttributes.Public | TypeAttributes.Sealed;
        }

        public string ClassName { get; }

        public string OutputFilePath { get; private set; }

        /// <summary>
        /// Method to add an attribute to the model class
        /// </summary>
        public void AddClassAttribute(AttributeModel attribute)
        {
            if (attribute.Arguments != null && attribute.Arguments.Length != 0)
            {
                this.targetClass.CustomAttributes.Add(
                    new CodeAttributeDeclaration(
                        attribute.Name,
                        attribute.GetArguments()));
            }
            else
            {
                this.targetClass.CustomAttributes.Add(new CodeAttributeDeclaration(attribute.Name));
            }
        }

        /// <summary>
        /// Method to provide easy addition of get/set 'autoproperties'.
        /// </summary>
        public void AddProperty(string propertyName, Type propType)
        {
            this.AddPropertyWithAttributes(propertyName, propType, null);
        }

        public void AddPropertyWithAttributes(string propertyName, Type propType, IList<AttributeModel> attributes)
        {
            // Add backing field since we can't use AUTOPROPERTIES OMGWTF!?!?!
            CodeMemberField backingField = new CodeMemberField();
            backingField.Attributes = MemberAttributes.Private;
            backingField.Name = $"_{propertyName}";
            backingField.Type = new CodeTypeReference(propType);
            this.targetClass.Members.Add(backingField);

            CodeMemberProperty newProperty = new CodeMemberProperty();
            newProperty.Attributes =
                MemberAttributes.Public | MemberAttributes.Final;

            newProperty.Name = propertyName;
            newProperty.HasGet = true;
            newProperty.HasSet = true;
            newProperty.Type = new CodeTypeReference(propType);
            newProperty.GetStatements.Add(new CodeMethodReturnStatement(
                new CodeFieldReferenceExpression(
                new CodeThisReferenceExpression(), backingField.Name)));
            newProperty.SetStatements.Add(new CodeAssignStatement(
                new CodeFieldReferenceExpression(
                    new CodeThisReferenceExpression(), backingField.Name),
                new CodeVariableReferenceExpression("value")));

            if (attributes != null)
            {
                foreach (AttributeModel attribute in attributes)
                {
                    newProperty.CustomAttributes.Add(
                        new CodeAttributeDeclaration(
                            attribute.Name, 
                            attribute.GetArguments()));
                }
            }

            this.targetClass.Members.Add(newProperty);
        }

        /// <summary>
        /// Writes this class out to a .cs file.
        /// </summary>
        public bool GenerateCCsharpClass(CodeDomNamespace namesp)
        {
            // Generate the code with the C# code provider.
            CSharpCodeProvider provider = new CSharpCodeProvider();
            CodeCompileUnit compileUnit = new CodeCompileUnit();

            // Add the application models to the root namespace and stage for compilation
            CodeNamespace newNamepsace = namesp.ConvertToCodeNamespace();
            newNamepsace.Types.Add(this.targetClass);
            compileUnit.Namespaces.Add(newNamepsace);
            
            string sourceFile;
            if (provider.FileExtension[0] == '.')
            {
                sourceFile = $"{this.ClassName}{provider.FileExtension}";
            }
            else
            {
                sourceFile = $"{this.ClassName}.{provider.FileExtension}";
            }

            this.OutputFilePath = this.filePath + sourceFile;

            // Create a TextWriter to a StreamWriter to the output file.
            using (StreamWriter sw = new StreamWriter(this.OutputFilePath, false))
            {
                IndentedTextWriter tw = new IndentedTextWriter(sw, "    ");

                // Generate source code using the code provider.
                provider.GenerateCodeFromCompileUnit(compileUnit, tw, new CodeGeneratorOptions());

                tw.Close();
            }

            this.isReadyToCompile = true;
            return true;
        }

        /// <summary>
        /// Compile this class into a single-class assembly. Mostly useful for unit testing. 
        /// See <see cref="CodeDomAssemblyCoordinator"/> for more typical compilation.
        /// </summary>
        public bool CompileClassFromGeneratedCsFile(CodeDomNamespace namesp)
        {
            if (!this.isReadyToCompile) this.GenerateCCsharpClass(namesp);

            return CompilationUtilities.CompileAssemblyFromFiles(
                this.filePath,
                namesp.Name,
                new[] { this.OutputFilePath },
                namesp);
        }
    }
}
