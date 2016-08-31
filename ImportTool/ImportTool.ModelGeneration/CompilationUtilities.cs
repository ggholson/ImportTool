namespace ImportTool.ModelGeneration
{
    using System;
    using System.CodeDom;
    using System.CodeDom.Compiler;

    using Microsoft.CSharp;

    public static class CompilationUtilities
    {
        public static bool CompileAssemblyFromFiles(string outputPath, string outputAssemblyName, string[] files, CodeDomNamespace namesp)
        {
            CSharpCodeProvider provider = new CSharpCodeProvider();

            // Build the parameters for source compilation.
            CompilerParameters cp = new CompilerParameters();

            // Add an assembly reference.
            cp.ReferencedAssemblies.AddRange(namesp.GetAssemblyReferences());

            // Set the assembly file name to generate.
            cp.OutputAssembly = $"{outputPath}{outputAssemblyName}.dll";//RootNamespace;

            // Save the assembly as a physical file.
            cp.GenerateInMemory = false;

            // Invoke compilation.
            CompilerResults cr = provider.CompileAssemblyFromFile(cp, files);

            if (cr.Errors.Count > 0)
            {
                // Display compilation errors.
                Console.WriteLine("Errors building {0} into {1}",
                    cp.OutputAssembly, cr.PathToAssembly);
                foreach (CompilerError ce in cr.Errors)
                {
                    Console.WriteLine("  {0}", ce.ToString());
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("Source {0} built into {1} successfully.",
                    cp.OutputAssembly, cr.PathToAssembly);
            }

            // Return the results of compilation.
            if (cr.Errors.Count > 0)
            {
                return false;
            }

            return true;
        }
    }
}
