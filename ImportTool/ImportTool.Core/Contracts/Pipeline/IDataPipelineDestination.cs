﻿namespace ImportTool.Core.Contracts.Pipeline
{
    /// <summary>
    /// Defines a class which must be able to accept data from an IDataPipelineSource
    /// </summary>
    public interface IDataPipelineDestination
    {
        void SetSource(IDataPipelineSource source);
    }
}
