using System;
using System.Collections.Concurrent;
using CVB.NET.Abstractions.Ioc.Injection.Lambda;
using RadFramework.Libraries.Ioc.Base;
using RadFramework.Libraries.Ioc.Registrations;

namespace RadFramework.Libraries.Ioc.Container
{
    public class SimpleContainer
    {
        private IDependencyInjectionLambdaGenerator lambdaGenerator = new DependencyInjectionLambdaGenerator();
        
        private ConcurrentDictionary<Type, RegistrationBase> registrations = new ConcurrentDictionary<Type, RegistrationBase>();

        public void Register(Type tInterface, Type tImplementation)
        {
            registrations[tInterface] = new IocImplementationRegistration(tImplementation, lambdaGenerator, this);
        }
        
        public void Register(Type tImplementation)
        {
            registrations[tImplementation] = new IocImplementationRegistration(tImplementation, lambdaGenerator, this);
        }

        public void RegisterSemiAutomatic(Type tImplementation, Func<object> construct)
        {
            registrations[tImplementation] = new FactoryRegistration(construct, this);
        }

        public T Resolve<T>()
        {
            return (T)Resolve(typeof(T));
        }
        
        public object Resolve(Type t)
        {
            return registrations[t].ResolveService(t);
        }
    }
}