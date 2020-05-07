using System;

namespace RadFramework.Libraries.Ioc.Base
{
    public abstract class RegistrationBase<TServiceKey>
    {
        public abstract object ResolveService(TServiceKey serviceKey);
    }
}