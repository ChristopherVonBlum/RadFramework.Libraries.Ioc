using RadFramework.Libraries.Ioc;

namespace RadFramework.Abstractions.Ioc
{
    public interface IServiceSetup<TServiceKey>
    {
        void Install(IServiceContainer<TServiceKey> serviceContainer);
    }
}