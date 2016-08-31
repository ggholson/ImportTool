namespace ImportTool.Core.Contracts
{
    using System;
    using System.Collections.Generic;

    public interface IDataSource : IDisposable
    {
        void Initialize();
        IEnumerable<T> ReadAll<T>();
        T ReadNext<T>();
    }
}
