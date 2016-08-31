namespace ImportTool.ModelGeneration.Test
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;

    using FastMember;

    using ImportTool.ModelGeneration.Models;

    using NUnit.Framework;

    [TestFixture]
    public class CodeDomClassModelTests
    {
        private readonly string className = "CodeDomTestClass";
		private readonly string csFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Files", "Generated");
        private readonly CodeDomNamespace namesp = new CodeDomNamespace("ImportTool.ModelGeneration.Generated.ClassModelTests");

        [OneTimeSetUp]
        public void SetUp()
        {
            if (!Directory.Exists(this.csFilePath))
            {
                Directory.CreateDirectory(this.csFilePath);
            }

            foreach (var file in Directory.GetFiles(this.csFilePath))
            {
                File.Delete(file);
            }
        }

        [Test]
        public void CanCreateCsFile()
        {
            string finalFileName = this.csFilePath + className + ".cs";

            CodeDomClassModel generator = new CodeDomClassModel(this.csFilePath, className);

            generator.GenerateCCsharpClass(this.namesp);

            Assert.IsTrue(File.Exists(finalFileName));
        }

        [Test]
        public void CanCompileGeneratedFile()
        {
           
            CodeDomClassModel generator = new CodeDomClassModel(this.csFilePath, className);

            generator.CompileClassFromGeneratedCsFile(this.namesp);

            string assemblyName = this.csFilePath + this.namesp.Name + ".dll";

            Assert.IsTrue(File.Exists(assemblyName));
        }

        [Test]
        public void CanInstantiateClassFromGeneratedFile()
        {
            CodeDomClassModel generator = new CodeDomClassModel(this.csFilePath, className);

            generator.CompileClassFromGeneratedCsFile(this.namesp);

            string assemblyName = this.csFilePath + this.namesp.Name + ".dll";

            Assembly generatedAssembly = Assembly.LoadFile(assemblyName);
            Type generatedType = generatedAssembly.GetType($"{namesp.Name}.{className}");
            object generatedObject = Activator.CreateInstance(generatedType);
            Assert.IsNotNull(generatedObject);
        }

        [Test]
        public void CanAddPropertyToGeneratedModel()
        {
            string propName = "Id";

            CodeDomClassModel generator = new CodeDomClassModel(this.csFilePath, className);
            generator.AddProperty(propName, typeof(int));

            generator.CompileClassFromGeneratedCsFile(this.namesp);

            string assemblyName = this.csFilePath + this.namesp.Name + ".dll";

            Assembly generatedAssembly = Assembly.LoadFile(assemblyName);
            Type generatedType = generatedAssembly.GetType($"{namesp.Name}.{className}");

            var accessor = TypeAccessor.Create(generatedType);
            object generatedObject = Activator.CreateInstance(generatedType);

            accessor[generatedObject, propName] = 9999;

            object result = accessor[generatedObject, propName];

            Assert.AreEqual(9999, (int) result);
        }

        [Test]
        public void CanAddAttributeToGeneratedModelClass()
        {
            CodeDomClassModel generator = new CodeDomClassModel(this.csFilePath, "ClassAttributeTest");
            generator.AddClassAttribute(new AttributeModel("Serializable"));
            generator.AddClassAttribute(new AttributeModel("XmlRoot", new object[] { "add" } ));
            this.namesp.References.Add(NamespaceReference.System);
            this.namesp.References.Add(NamespaceReference.SystemXmlSerialization);

            generator.GenerateCCsharpClass(this.namesp);

            using (StreamReader reader = File.OpenText($"{this.csFilePath}ClassAttributeTest.cs"))
            {
                string fileContents = reader.ReadToEnd();
                Assert.IsTrue(fileContents.Contains("[Serializable()]"));
                Assert.IsTrue(fileContents.Contains("[XmlRoot(\"add\")]"));
            }

            generator.CompileClassFromGeneratedCsFile(this.namesp);
            
            string assemblyName = this.csFilePath + this.namesp.Name + ".dll";
            
            Assembly generatedAssembly = Assembly.LoadFile(assemblyName);
            Type generatedType = generatedAssembly.GetType($"{namesp.Name}.ClassAttributeTest");

            Assert.IsNotNull(generatedType);
        }

        [Test]
        public void CanAddAttributeToGeneratedProperty()
        {
            CodeDomClassModel generator = new CodeDomClassModel(this.csFilePath, "PropAttributeTest");
            generator.AddClassAttribute(new AttributeModel("Serializable"));
            generator.AddPropertyWithAttributes(
                "Test", 
                typeof(string), 
                new List<AttributeModel> { new AttributeModel("XmlElement", new object[] { "Test" }) }
                );

            this.namesp.References.Add(NamespaceReference.System);
            this.namesp.References.Add(NamespaceReference.SystemXmlSerialization);

            generator.GenerateCCsharpClass(this.namesp);

            using (StreamReader reader = File.OpenText($"{this.csFilePath}PropAttributeTest.cs"))
            {
                string fileContents = reader.ReadToEnd();
                Assert.IsTrue(fileContents.Contains("[XmlElement(\"Test\")]"));
            }

            generator.CompileClassFromGeneratedCsFile(this.namesp);

            string assemblyName = this.csFilePath + this.namesp.Name + ".dll";

            Assembly generatedAssembly = Assembly.LoadFile(assemblyName);
            Type generatedType = generatedAssembly.GetType($"{namesp.Name}.PropAttributeTest");

            Assert.IsNotNull(generatedType);
        }
    }
}
