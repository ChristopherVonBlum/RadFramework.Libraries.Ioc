using System;

namespace RadFramework.Abstractions.Ioc
{
    public interface IServiceInstaller<TServiceKey>
    {
        void RegisterServiceImplementation(TServiceKey serviceKey, Type tImplementation);
        void RegisterServiceInstance(TServiceKey serviceKey, object instance);
        void RegisterLazySingleton(TServiceKey serviceKey, Func<object> getInstance);

        /// <summary>
        /// Will bind the invocation of factoryFunc to all resolve calls that match a certain serviceKey.
        /// </summary>
        /// <param name="serviceKey"></param>
        /// <param name="factoryFunc"></param>
        void RegisterServiceFactory(TServiceKey serviceKey, Func<TServiceKey, object> factoryFunc);
    }
}