using System;

using RadFramework.Libraries.Ioc.Base;

namespace RadFramework.Libraries.Ioc.Registrations
{
    class SingletonInstanceRegistration : RegistrationBase
    {
        private object instance;

        public SingletonInstanceRegistration(object instance)
        {
            this.instance = instance;
        }
        
        public override object ResolveService()
        {
            return instance;
        }
    }
}