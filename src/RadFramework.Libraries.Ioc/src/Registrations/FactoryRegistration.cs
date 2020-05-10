using System;
using CVB.NET.Abstractions.Ioc.Injection.Parameter;
using RadFramework.Libraries.Ioc.Base;
using RadFramework.Libraries.Ioc.Container;

namespace RadFramework.Libraries.Ioc.Registrations
{
    class FactoryRegistration : RegistrationBase
    {
        private readonly Func<object> factoryFunc;
        private readonly SimpleContainer container;

        public FactoryRegistration(Func<object> factoryFunc, SimpleContainer container)
        {
            this.factoryFunc = factoryFunc;
            this.container = container;
        }
        
        public override object ResolveService(Type serviceKey)
        {
            using (Arg.UseContextualResolver(container.Resolve))
            {
                return factoryFunc();
            }
        }
    }
}