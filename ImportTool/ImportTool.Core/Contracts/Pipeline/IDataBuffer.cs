namespace ImportTool.Core.Contracts.Pipeline
{
    public interface IDataBuffer
    {
        object this[string key] { get; set; }

        void AddRow();

        void Flush();
    }
}
