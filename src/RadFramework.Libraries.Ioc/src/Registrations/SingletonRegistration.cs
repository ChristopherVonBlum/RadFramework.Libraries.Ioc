using System;
using CVB.NET.Abstractions.Ioc.Injection.Lambda;
using RadFramework.Libraries.Ioc.Container;
using RadFramework.Libraries.Reflection.Caching;

namespace RadFramework.Libraries.Ioc.Registrations
{
    public class SingletonRegistration : TransientRegistration
    {
        private Lazy<object> singleton;
        public SingletonRegistration(CachedType tImplementation, IDependencyInjectionLambdaGenerator lambdaGenerator, Container.Container container) : base(tImplementation, lambdaGenerator, container)
        {
            singleton = new Lazy<object>(() => base.ResolveService());
        }
        
        public override object ResolveService()
        {
            return singleton.Value;
        }
    }
}