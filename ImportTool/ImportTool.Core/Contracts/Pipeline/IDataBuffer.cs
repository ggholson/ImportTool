namespace ImportTool.Core.Contracts.Pipeline
{
    using System.Collections.Generic;

    public interface IDataBuffer<T>
    {
        bool IsEmpty { get; }

        void PushRow(T row);

        T RetrieveNextRow();

        IList<T> Flush();
    }
}
