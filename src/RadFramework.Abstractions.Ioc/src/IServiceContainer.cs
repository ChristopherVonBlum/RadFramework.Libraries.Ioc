using System;

using RadFramework.Abstractions.Ioc;

namespace RadFramework.Libraries.Ioc
{
    public interface IServiceContainer<TServiceKey> : IServiceLocator<TServiceKey>, IServiceInstaller<TServiceKey>, IDisposable
    {
        void AttachPrecedingFallbackContainer(IServiceContainer<TServiceKey> container);
        void AttachAppendingFallbackContainer(IServiceContainer<TServiceKey> container);
    }
}
