namespace ImportTool.Core.DataAccess.Buffers
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Threading.Tasks.Dataflow;

    using ImportTool.Core.Contracts.Pipeline;

    public class GenericDataBuffer<T> : IDataBuffer<T>, IDataPipelineDestination<T>, IDisposable
    {
        //private readonly BlockingCollection<T> backingCollection;
        private readonly BufferBlock<T> backingCollection;

        public GenericDataBuffer()
        {
            this.backingCollection = //new BlockingCollection<T>();
        }

        public bool IsEmpty { get; private set; }

        public void PushRow(T rowObject)
        {
            this.backingCollection.Add(rowObject);
        }

        public T RetrieveNextRow()
        {
            T dequeuedRow;

            bool hasNextRow = this.backingCollection.TryTake(out dequeuedRow);

            if (!hasNextRow) throw new Exception("Invalid attempt to read from empty data buffer!");

            return dequeuedRow;
        }

        public IList<T> Flush()
        {
            return this.backingCollection.ToList();
        }

        public async Task<bool> RowAvailableAsync()
        {
            
        }

        public void Dispose()
        {
            this.backingCollection.Dispose();
        }

        public void PreExecute()
        {
        }

        public void PostExecute()
        {
        }
    }
}
