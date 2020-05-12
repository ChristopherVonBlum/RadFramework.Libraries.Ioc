using System;
using System.Collections.Concurrent;
using System.Linq;
using RadFramework.Libraries.Ioc.Factory;
using RadFramework.Libraries.Ioc.Registrations;
using RadFramework.Libraries.Reflection.Caching.Queries;

namespace RadFramework.Libraries.Ioc
{
    public class Container : IServiceProvider
    {
        internal readonly InjectionOptions injectionOptions;
        protected ServiceFactoryLambdaGenerator LambdaGenerator { get; } = new ServiceFactoryLambdaGenerator();
        
        private ConcurrentDictionary<Type, RegistrationBase> registrations = new ConcurrentDictionary<Type, RegistrationBase>();

        public Container(InjectionOptions injectionOptions)
        {
            this.injectionOptions = injectionOptions;
        }

        public Container()
        {
            this.injectionOptions = new InjectionOptions
            {
                ChooseInjectionConstructor = ctors => ctors
                        .OrderByDescending(c => c.Query(MethodBaseQueries.GetParameters).Length)
                        .First()
            };
        }
        
        public RegistrationBase RegisterTransient(Type tInterface, Type tImplementation)
        {
            return registrations[tInterface] = new TransientRegistration(tImplementation, LambdaGenerator, this)
            {
                InjectionOptions = injectionOptions.Clone()
            };
        }

        public RegistrationBase RegisterTransient<TInterface, TImplementation>()
        {
            return registrations[typeof(TInterface)] = new TransientRegistration(typeof(TImplementation), LambdaGenerator, this)
            {
                InjectionOptions = injectionOptions.Clone()
            };
        }

        public RegistrationBase RegisterTransient(Type tImplementation)
        {
            return registrations[tImplementation] = new TransientRegistration(tImplementation, LambdaGenerator, this)
            {
                InjectionOptions = injectionOptions.Clone()
            };
        }
        
        public RegistrationBase RegisterTransient<TImplementation>()
        {
            Type tImplementation = typeof(TImplementation);
            return registrations[tImplementation] = new TransientRegistration(tImplementation, LambdaGenerator, this)
            {
                InjectionOptions = injectionOptions.Clone()
            };
        }

        public void RegisterSemiAutomaticTransient(Type tImplementation, Func<Container, object> construct)
        {
            registrations[tImplementation] = new TransientFactoryRegistration(construct, this);
        }
        
        public void RegisterSemiAutomaticTransient<TImplementation>(Func<Container, object> construct)
        {
            registrations[typeof(TImplementation)] = new TransientFactoryRegistration(construct, this);
        }

        
        public RegistrationBase RegisterSingleton(Type tInterface, Type tImplementation)
        {
            return registrations[tInterface] = new SingletonRegistration(tImplementation, LambdaGenerator, this)
            {
                InjectionOptions = injectionOptions.Clone()
            };
        }

        public RegistrationBase RegisterSingleton<TInterface, TImplementation>()
        {
            return registrations[typeof(TInterface)] = new SingletonRegistration(typeof(TImplementation), LambdaGenerator, this)
            {
                InjectionOptions = injectionOptions.Clone()
            };
        }

        public RegistrationBase RegisterSingleton(Type tImplementation)
        {
            return registrations[tImplementation] = new SingletonRegistration(tImplementation, LambdaGenerator, this)
            {
                InjectionOptions = injectionOptions.Clone()
            };
        }
        
        public RegistrationBase RegisterSingleton<TImplementation>()
        {
            Type tImplementation = typeof(TImplementation);
            return registrations[tImplementation] = new SingletonRegistration(tImplementation, LambdaGenerator, this)
            {
                InjectionOptions = injectionOptions.Clone()
            };
        }

        public void RegisterSemiAutomaticSingleton(Type tImplementation, Func<Container, object> construct)
        {
            registrations[tImplementation] = new SingletonFactoryRegistration(construct, this);
        }
        
        public void RegisterSemiAutomaticSingleton<TImplementation>(Func<Container, object> construct)
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

        public object GetService(Type serviceType)
        {
            return Resolve(serviceType);
        }
    }
}