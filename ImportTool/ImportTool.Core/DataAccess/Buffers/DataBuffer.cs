namespace ImportTool.Core.DataAccess.Buffers
{
    using System.Collections.Generic;

    using ImportTool.Core.Contracts.Pipeline;

    public class DataBuffer : IDataBuffer, IDataPipelineElement
    {
        private IDictionary<string, object> currentRow;

        public object this[string key]
        {
            get
            {
                return currentRow[key];
            }
            set
            {
                currentRow[key] = value;
            }
        }

        public void AddRow()
        {
            throw new System.NotImplementedException();
        }

        public void Flush()
        {
            throw new System.NotImplementedException();
        }

        public void SetSource(IDataPipelineSource source)
        {
            throw new System.NotImplementedException();
        }
    }
}
