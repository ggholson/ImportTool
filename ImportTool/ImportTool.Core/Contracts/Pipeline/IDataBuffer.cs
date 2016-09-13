namespace ImportTool.Core.Contracts.Pipeline
{
    using System;
    using System.Collections.Generic;

    public interface IDataBuffer<T>
    {
        /// <summary>
        /// Tells any subsequent listening components that there is data
        /// available for consumption from this buffer.
        /// </summary>
        event EventHandler CanBeRead;

        bool IsEmpty { get; }

        void CompletedAdding();

        void PushRow(T row);

        T RetrieveNextRow();
    }
}
