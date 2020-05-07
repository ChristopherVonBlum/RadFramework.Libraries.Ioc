using System;

using RadFramework.Libraries.Ioc.Base;

namespace RadFramework.Libraries.Ioc.Registrations
{
    class InstanceRegistration<TServiceKey> : RegistrationBase<TServiceKey>
    {
        private Lazy<object> lazyContainer;
        private Func<object> instanceContainer;
        
        public InstanceRegistration(WeakReference instance)
        {
            this.instanceContainer = () => instance.IsAlive ? instance.Target : null;
        }

        public InstanceRegistration(object instance)
        {
            this.instanceContainer = () => instance;
        }

        public InstanceRegistration(Func<object> getInstance)
        {
            this.lazyContainer = new Lazy<object>(getInstance);
            this.instanceContainer = () => this.lazyContainer.Value;
        }
        
        public override object ResolveService(TServiceKey serviceKey)
        {
            return this.instanceContainer();
        }
    }
}