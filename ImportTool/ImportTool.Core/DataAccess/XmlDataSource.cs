namespace ImportTool.Core.DataAccess
{
    using System.Collections.Generic;

    using ImportTool.Core.Contracts;

    public abstract class XmlDataSource
    {
        protected string FilePath { get; }

        public XmlDataSource(string path)
        {
            this.FilePath = path;
        }
    }
}
