using System;

using RadFramework.Libraries.Ioc.Base;

namespace RadFramework.Libraries.Ioc.Registrations
{
    public class IocImplementationRegistration<TServiceContainer, TServiceKey> : RegistrationBase<TServiceKey> 
        where TServiceContainer : IServiceContainer<TServiceKey>
    {
        private readonly Type tImplementation;

        private readonly TServiceContainer container;

        public IocImplementationRegistration(Type tImplementation, TServiceContainer container)
        {
            this.tImplementation = tImplementation;
            this.container = container;
        }

        public override object ResolveService(TServiceKey serviceKey)
        {
            throw new NotImplementedException();
        }
    }
}