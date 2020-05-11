using System;
using RadFramework.Libraries.Ioc.Container;

namespace RadFramework.Libraries.Ioc.Registrations
{
    public class SingletonFactoryRegistration : TransientFactoryRegistration
    {
        private Lazy<object> singleton;

        public SingletonFactoryRegistration(Func<object> factoryFunc, Container.Container container) : base(factoryFunc, container)
        {
            singleton = new Lazy<object>(() => base.ResolveService());
        }

        public override object ResolveService()
        {
            return singleton.Value;
        }
    }
}