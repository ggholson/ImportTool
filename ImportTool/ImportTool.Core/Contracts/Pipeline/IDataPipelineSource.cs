namespace ImportTool.Core.Contracts.Pipeline
{
    public interface IDataPipelineSource
    {
        IDataPipelineDestination PipelineDestination { get; }

        void SetDestination(IDataPipelineDestination destination);
    }
}
