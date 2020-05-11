using System;
using CVB.NET.Abstractions.Ioc.Injection.Parameter;
using RadFramework.Libraries.Ioc.Base;
using RadFramework.Libraries.Ioc.Container;

namespace RadFramework.Libraries.Ioc.Registrations
{
    public class TransientFactoryRegistration : RegistrationBase
    {
        private readonly Func<Container.Container, object> factoryFunc;
        private readonly Container.Container container;

        public TransientFactoryRegistration(Func<Container.Container, object> factoryFunc, Container.Container container)
        {
            this.factoryFunc = factoryFunc;
            this.container = container;
        }
        
        public override object ResolveService()
        {
            using (Arg.UseContextualResolver(container.Resolve))
            {
                return factoryFunc(container);
            }
        }
    }
}