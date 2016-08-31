namespace ImportTool.Core.DataAccess
{
    using System.Collections.Generic;
    using System.Xml.Linq;

    using ImportTool.Core.Contracts;

    public class XmlInMemoryDataSource
    {
        private XDocument file;
        private string filePath;

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }

        public void Initialize()
        {
            throw new System.NotImplementedException();
        }

        public T Read<T>(int id)
        {
            throw new System.NotImplementedException();
        }

        public T Read<T>(string field, string value)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<T> ReadAll<T>()
        {
            throw new System.NotImplementedException();
        }
    }
}
