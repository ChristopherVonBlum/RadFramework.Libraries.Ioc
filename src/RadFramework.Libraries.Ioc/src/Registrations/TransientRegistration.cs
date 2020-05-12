using System;
using System.Linq;
using RadFramework.Libraries.Ioc.Factory;
using RadFramework.Libraries.Reflection.Caching;
using RadFramework.Libraries.Reflection.Caching.Queries;

namespace RadFramework.Libraries.Ioc.Registrations
{
    public class TransientRegistration : RegistrationBase
    {
        private readonly Container container;

        private readonly Lazy<Func<Container, object>> construct;
        
        public TransientRegistration(CachedType tImplementation,
            ServiceFactoryLambdaGenerator lambdaGenerator, Container container)
        {
            this.container = container;

            this.construct = new Lazy<Func<Container, object>>(() => lambdaGenerator.CreateInstanceFactory(tImplementation, container.injectionOptions, InjectionOptions));
        }

        public override object ResolveService()
        {
            return construct.Value(container);
        }
    }
}