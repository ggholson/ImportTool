namespace ImportTool.Core.Contracts.Pipeline
{
    /// <summary>
    /// Interface defining an element which can be connected with other 
    /// elements implementing IDataPipelineElement to form a Data Pipeline
    /// </summary>
    public interface IDataPipelineElement : IDataPipelineSource, IDataPipelineDestination
    {
    }
}
