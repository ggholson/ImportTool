namespace ImportTool.Core.Contracts.Pipeline
{
    public interface IDataProcessingComponent<in T> : IDataPipelineComponent, IDataPipelineSource, IDataPipelineDestination<T>
    {
        void BeforeEachRow();

        void ProcessRow();

        void AfterEachRow();
    }
}
