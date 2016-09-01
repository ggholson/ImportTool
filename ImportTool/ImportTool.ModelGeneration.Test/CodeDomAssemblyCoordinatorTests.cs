namespace ImportTool.ModelGeneration.Test
{
    using System;
    using System.CodeDom;
    using System.IO;
    using System.Reflection;

    using ImportTool.ModelGeneration.Models;

    using NUnit.Framework;

    [TestFixture]
    public class CodeDomAssemblyCoordinatorTests
    {
        private readonly string className = "AssemblyCoordinatorTestClass";
        private readonly string csFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"Files","Generated");
        private readonly CodeNamespace namesp = new CodeNamespace("ImportTool.ModelGeneration.Generated");

        [Test]
        public void CanInstantiateModelFromCoordinator()
        {
            CodeDomAssemblyCoordinator coordinator = new CodeDomAssemblyCoordinator(this.csFilePath, "Generated");
            CodeDomClassModel model = coordinator.CreateClassModel(this.className);

            Assert.IsNotNull(model);
        }

        [Test]
        public void ClassCanBeCompiledFromCoordinator()
        {
            string propName = "Id";
            string assemblyName = Path.Combine(this.csFilePath, "AssemblyCoordinatorTests.dll");

            CodeDomAssemblyCoordinator coordinator = new CodeDomAssemblyCoordinator(this.csFilePath, "AssemblyCoordinatorTests");
            CodeDomClassModel model = coordinator.CreateClassModel(this.className);
            model.AddProperty(propName, typeof(int));

            coordinator.StageModelForCompilation(model);
            coordinator.AddNamespaceImport(NamespaceReference.SystemXml);

            coordinator.CompileCSharpCode();

            Assert.IsTrue(File.Exists(Path.Combine(this.csFilePath, $"{this.className}.cs")));
            Assert.IsTrue(File.Exists(assemblyName));

            Assembly generatedAssembly = Assembly.LoadFile(assemblyName);
            Type generatedType = generatedAssembly.GetType($"{namesp.Name}.{className}");
            object generatedObject = Activator.CreateInstance(generatedType);
            Assert.IsNotNull(generatedObject);
        }

        [Test]
        public void CanCompileMultipleClassesIntoAssembly()
        {
            string propName = "Id";
			string assemblyName = Path.Combine(this.csFilePath, "AssemblyCoordinatorTests.dll");
            string secondaryClassName = "DifferentTestClass";

            CodeDomAssemblyCoordinator coordinator = new CodeDomAssemblyCoordinator(this.csFilePath, "AssemblyCoordinatorTests");
            CodeDomClassModel model = coordinator.CreateClassModel(this.className);
            model.AddProperty(propName, typeof(int));
            coordinator.StageModelForCompilation(model);

            CodeDomClassModel model2 = coordinator.CreateClassModel(secondaryClassName);
            model2.AddProperty("StringProperty", typeof(string));
            coordinator.StageModelForCompilation(model2);

            coordinator.CompileCSharpCode();

            Assert.IsTrue(File.Exists(Path.Combine(this.csFilePath, $"{this.className}.cs")));
            Assert.IsTrue(File.Exists(Path.Combine(this.csFilePath, $"{secondaryClassName}.cs")));
            Assert.IsTrue(File.Exists(assemblyName));

            Assembly generatedAssembly = Assembly.LoadFile(assemblyName);

            Type generatedType = generatedAssembly.GetType($"{namesp.Name}.{className}");
            object generatedObject = Activator.CreateInstance(generatedType);
            Assert.IsNotNull(generatedObject);

            Type generatedType2 = generatedAssembly.GetType($"{namesp.Name}.{secondaryClassName}");
            object generatedObject2 = Activator.CreateInstance(generatedType2);
            Assert.IsNotNull(generatedObject2);
        }

        [Test]
        public void CanAddNamespaceReferenceToAssembledFiles()
        {
            string secondaryClassName = "DifferentTestClass";

            CodeDomAssemblyCoordinator coordinator = new CodeDomAssemblyCoordinator(this.csFilePath, "AssemblyCoordinatorTests");
            CodeDomClassModel model = coordinator.CreateClassModel(this.className);
            coordinator.StageModelForCompilation(model);

            coordinator.AddNamespaceImport(NamespaceReference.SystemXml);

            CodeDomClassModel model2 = coordinator.CreateClassModel(secondaryClassName);
            coordinator.StageModelForCompilation(model2);

            coordinator.CompileCSharpCode();

            using (StreamReader reader = File.OpenText(Path.Combine(this.csFilePath, $"{this.className}.cs")))
            {
                string fileContents = reader.ReadToEnd();
                Assert.IsTrue(fileContents.Contains("using System;"));
                Assert.IsTrue(fileContents.Contains("using System.Xml;"));
            }

            using (StreamReader reader2 = File.OpenText(Path.Combine(this.csFilePath, $"{secondaryClassName}.cs")))
            {
                string fileContents = reader2.ReadToEnd();
                Assert.IsTrue(fileContents.Contains("using System;"));
                Assert.IsTrue(fileContents.Contains("using System.Xml;"));
            }
        }
    }
}
