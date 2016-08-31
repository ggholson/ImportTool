namespace ImportTool.Core.Contracts
{
    public interface IDataRepository
    {
        T Create<T>();

        T Read<T>();

        T Update<T>();

        T Delete<T>();
    }
}
