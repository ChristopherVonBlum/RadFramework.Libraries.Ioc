using System;

using RadFramework.Libraries.Ioc.Base;

namespace RadFramework.Libraries.Ioc.Registrations
{
    class InstanceRegistration<TServiceKey> : RegistrationBase
    {
        private Lazy<object> lazyContainer;
        private Func<object> instanceContainer;
        
        public InstanceRegistration(WeakReference instance)
        {
            instanceContainer = () => instance.IsAlive ? instance.Target : null;
        }

        public InstanceRegistration(object instance)
        {
            instanceContainer = () => instance;
        }

        public InstanceRegistration(Func<object> getInstance)
        {
            lazyContainer = new Lazy<object>(getInstance);
            instanceContainer = () => lazyContainer.Value;
        }
        
        public override object ResolveService(Type serviceKey)
        {
            return instanceContainer();
        }
    }
}