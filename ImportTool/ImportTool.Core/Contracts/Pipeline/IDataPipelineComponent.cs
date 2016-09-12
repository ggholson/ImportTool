namespace ImportTool.Core.Contracts.Pipeline
{
    /// <summary>
    /// Interface defining an element which can be connected with other 
    /// elements implementing IDataPipelineComponent to form a Data Pipeline
    /// </summary>
    public interface IDataPipelineComponent
    {
        /// <summary>
        /// Execute once before any data is processed in this component
        /// </summary>
        void PreExecute();

        /// <summary>
        /// Execute once after all data has been processed by this component
        /// </summary>
        void PostExecute();
    }
}
