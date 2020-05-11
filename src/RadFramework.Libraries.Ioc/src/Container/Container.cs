using System;
using System.Collections.Concurrent;
using CVB.NET.Abstractions.Ioc.Injection.Lambda;
using RadFramework.Libraries.Ioc.Base;
using RadFramework.Libraries.Ioc.Registrations;

namespace RadFramework.Libraries.Ioc.Container
{
    public class Container
    {
        private IDependencyInjectionLambdaGenerator lambdaGenerator = new DependencyInjectionLambdaGenerator();
        
        private ConcurrentDictionary<Type, RegistrationBase> registrations = new ConcurrentDictionary<Type, RegistrationBase>();

        public void RegisterTransient(Type tInterface, Type tImplementation)
        {
            registrations[tInterface] = new TransientRegistration(tImplementation, lambdaGenerator, this);
        }

        public void RegisterTransient<TInterface, TImplementation>()
        {
            registrations[typeof(TInterface)] = new TransientRegistration(typeof(TImplementation), lambdaGenerator, this);
        }

        public void RegisterTransient(Type tImplementation)
        {
            registrations[tImplementation] = new TransientRegistration(tImplementation, lambdaGenerator, this);
        }
        
        public void RegisterTransient<TImplementation>()
        {
            Type tImplementation = typeof(TImplementation);
            registrations[tImplementation] = new TransientRegistration(tImplementation, lambdaGenerator, this);
        }

        public void RegisterSemiAutomaticTransient(Type tImplementation, Func<object> construct)
        {
            registrations[tImplementation] = new TransientFactoryRegistration(construct, this);
        }
        
        public void RegisterSemiAutomaticTransient<TImplementation>(Func<object> construct)
        {
            registrations[typeof(TImplementation)] = new TransientFactoryRegistration(construct, this);
        }

        
        public void RegisterSingleton(Type tInterface, Type tImplementation)
        {
            registrations[tInterface] = new SingletonRegistration(tImplementation, lambdaGenerator, this);
        }

        public void RegisterSingleton<TInterface, TImplementation>()
        {
            registrations[typeof(TInterface)] = new SingletonRegistration(typeof(TImplementation), lambdaGenerator, this);
        }

        public void RegisterSingleton(Type tImplementation)
        {
            registrations[tImplementation] = new SingletonRegistration(tImplementation, lambdaGenerator, this);
        }
        
        public void RegisterSingleton<TImplementation>()
        {
            Type tImplementation = typeof(TImplementation);
            registrations[tImplementation] = new SingletonRegistration(tImplementation, lambdaGenerator, this);
        }

        public void RegisterSemiAutomaticSingleton(Type tImplementation, Func<object> construct)
        {
            registrations[tImplementation] = new SingletonFactoryRegistration(construct, this);
        }
        
        public void RegisterSemiAutomaticSingleton<TImplementation>(Func<object> construct)
        {
            registrations[typeof(TImplementation)] = new SingletonFactoryRegistration(construct, this);
        }

        public void RegisterSingletonInstance(Type tInterface, object instance)
        {
            registrations[tInterface] = new SingletonInstanceRegistration(instance);
        }
        
        public void RegisterSingletonInstance<TInterface>(object instance)
        {
            registrations[typeof(TInterface)] = new SingletonInstanceRegistration(instance);
        }
        
        public void RegisterSingletonInstance<TImplementation>(TImplementation instance)
        {
            registrations[typeof(TImplementation)] = new SingletonInstanceRegistration(instance);
        }
        
        public T Resolve<T>()
        {
            return (T)Resolve(typeof(T));
        }
        
        public object Resolve(Type t)
        {
            return registrations[t].ResolveService();
        }
    }
}