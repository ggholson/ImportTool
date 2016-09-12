namespace ImportTool.Core.Contracts.Pipeline
{
    public interface IDataProcessingComponent : IDataPipelineComponent, IDataPipelineSource, IDataPipelineDestination
    {
        void BeforeEachRow();

        void ProcessRow();

        void AfterEachRow();
    }
}
