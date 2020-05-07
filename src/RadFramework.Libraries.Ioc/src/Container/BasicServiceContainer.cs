using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

using RadFramework.Abstractions.Ioc.Base.Exception;
using RadFramework.Libraries.Ioc;
using RadFramework.Libraries.Ioc.Base;
using RadFramework.Libraries.Ioc.Registrations;

namespace RadFramework.Abstractions.Ioc.Base
{
    public class BasicServiceContainer<TServiceKey> : IServiceContainer<TServiceKey>
    {
        protected ConcurrentDictionary<TServiceKey, RegistrationBase<TServiceKey>> ContainerData { get; } = new ConcurrentDictionary<TServiceKey, RegistrationBase<TServiceKey>>();

        protected List<IServiceContainer<TServiceKey>> FallbackLocators = new List<IServiceContainer<TServiceKey>>();
       
        public void AttachPrecedingFallbackContainer(IServiceContainer<TServiceKey> container)
        {
            this.FallbackLocators.Insert(0, container);
        }

        public void AttachAppendingFallbackContainer(IServiceContainer<TServiceKey> container)
        {
            this.FallbackLocators.Add(container);
        }
        
        public virtual bool CanLocate(TServiceKey serviceKey)
        {
            return this.ContainerData.ContainsKey(serviceKey)|| this.FallbackLocators.Any(s => s.CanLocate(serviceKey));
        }

        public virtual object Locate(TServiceKey serviceKey)
        {
            if (!this.CanLocate(serviceKey))
            {
                var locator = this.FallbackLocators.FirstOrDefault(l => l.CanLocate(serviceKey));

                if (locator == null)
                {
                    throw new CouldNotLocateServiceException();
                }

                return locator.Locate(serviceKey);
            }
            
            return this.ResolveInternal(serviceKey, this.ContainerData[serviceKey]);
        }

        public virtual void RegisterServiceImplementation(TServiceKey serviceKey, Type tImplementation)
        {
            this.ContainerData[serviceKey] = new IocImplementationRegistration<IServiceContainer<TServiceKey>, TServiceKey>(tImplementation, this);
        }

        public virtual void RegisterServiceInstance(TServiceKey serviceKey, object instance)
        {
            this.ContainerData[serviceKey] = new InstanceRegistration<TServiceKey>(instance);
        }

        public virtual void RegisterLazySingleton(TServiceKey serviceKey, Func<object> getInstance)
        {
            this.ContainerData[serviceKey] = new InstanceRegistration<TServiceKey>(getInstance);
        }

        public virtual void RegisterServiceFactory(TServiceKey serviceKey, Func<TServiceKey, object> factoryFunc)
        {
            this.ContainerData[serviceKey] = new FactoryRegistration<TServiceKey>((key) => factoryFunc(key));
        }

        protected virtual object ResolveInternal(TServiceKey serviceKey,
            RegistrationBase<TServiceKey> registrationBase)
        {
            return registrationBase.ResolveService(serviceKey);
        }
        
        public void Dispose()
        {
        }
    }
}