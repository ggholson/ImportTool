namespace ImportTool.Core
{
    using System.IO;

    public static class FileUtility
    {
        public static FileStream OpenStream(string path)
        {
            if (!File.Exists(path)) throw new FileNotFoundException($"The source file at {path} could not be located.");
            return File.Open(path, FileMode.Open);
        }
    }
}
