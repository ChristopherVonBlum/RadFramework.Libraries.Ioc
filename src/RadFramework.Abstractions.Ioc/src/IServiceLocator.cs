namespace RadFramework.Abstractions.Ioc
{
    public interface IServiceLocator<TServiceKey>
    {
        bool CanLocate(TServiceKey serviceKey);

        object Locate(TServiceKey serviceKey);
    }
}