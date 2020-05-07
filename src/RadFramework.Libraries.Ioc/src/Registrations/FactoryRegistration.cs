using System;

using RadFramework.Libraries.Ioc.Base;

namespace RadFramework.Libraries.Ioc.Registrations
{
    class FactoryRegistration<TServiceKey> : RegistrationBase<TServiceKey>
    {
        private readonly Func<TServiceKey, object> factoryFunc;

        public FactoryRegistration(Func<TServiceKey, object> factoryFunc)
        {
            this.factoryFunc = factoryFunc;
        }
        
        public override object ResolveService(TServiceKey serviceKey)
        {
            return this.factoryFunc(serviceKey);
        }
    }
}