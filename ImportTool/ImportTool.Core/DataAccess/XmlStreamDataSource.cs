namespace ImportTool.Core.DataAccess
{
    using System.Collections.Generic;
    using System.Xml;

    using ImportTool.Core.Contracts;

    /// <summary>
    /// Asynchronous, forward-only XML file reader which attempts to parse the 
    /// specified document into elements of type T.
    /// </summary>
    public class XmlStreamDataSource : XmlDataSource, IDataSource
    {
        private XmlReader reader;

        public XmlStreamDataSource(string path)
            : base(path)
        {
        }

        public void Dispose()
        {
            this.reader.Dispose();
        }

        public void Initialize()
        {
            var settings = new XmlReaderSettings
                               {
                                   Async = true,
                                   ConformanceLevel = ConformanceLevel.Fragment,
                                   ValidationType = ValidationType.None
                               };

            this.reader = XmlReader.Create(FileUtility.OpenStream(this.FilePath), settings);
        }

        public IEnumerable<T> ReadAll<T>()
        {
            throw new System.NotImplementedException();
        }

        public T ReadNext<T>()
        {
            throw new System.NotImplementedException();
        }
    }
}
