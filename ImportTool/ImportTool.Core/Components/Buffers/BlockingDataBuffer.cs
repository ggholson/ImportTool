namespace ImportTool.Core.Components.Buffers
{
    using System;
    using System.Collections.Concurrent;

    using ImportTool.Core.Contracts.Pipeline;

    /// <summary>
    /// Implementation of BlockingCollection as a data buffer for use connecting pipeline components
    /// where data needs to be arranged before consuming.
    /// Can be pushed by multiple threads, or read from by multiple threads, but can not be pushed to
    /// and read from simulaneously.
    /// </summary>
    public class BlockingDataBuffer<T> : IDataBuffer<T>, IDataPipelineDestination<T>
    {
        private readonly BlockingCollection<T> backingCollection;

        public BlockingDataBuffer()
        {
            this.backingCollection = new BlockingCollection<T>();
        }

        public event EventHandler CanBeRead;

        public bool IsEmpty
        {
            get
            {
                return this.backingCollection.Count <= 0;
            }
        }

        public void CompletedAdding()
        {
            this.backingCollection.CompleteAdding();

            CanBeRead?.Invoke(this, EventArgs.Empty);
        }

        public void PushRow(T rowObject)
        {
            if (this.backingCollection.IsAddingCompleted)
            {
                throw new InvalidOperationException("Buffer has been closed for writing");
            }

            this.backingCollection.Add(rowObject);
        }

        public T RetrieveNextRow()
        {
            if (!this.backingCollection.IsAddingCompleted)
            {
                throw new InvalidOperationException("BlockingBuffer is still being populated and is not ready for reading");
            }

            T dequeuedRow;
            bool hasNextRow = this.backingCollection.TryTake(out dequeuedRow);

            if (!hasNextRow) throw new InvalidOperationException("Invalid attempt to read from empty data buffer!");

            return dequeuedRow;
        }

        public void PreExecute()
        {
        }

        public void PostExecute()
        {
            this.backingCollection.Dispose();
        }
    }
}
