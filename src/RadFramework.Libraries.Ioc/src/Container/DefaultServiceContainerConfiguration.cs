using System;
using System.Collections.Generic;

using RadFramework.Libraries.Ioc.Base;

namespace RadFramework.Libraries.Ioc.Container
{
    [Serializable]
    public class DefaultServiceContainerConfiguration
    {
        public IDictionary<(Type tService, string serviceKey), RegistrationBase<(Type tService, string serviceKey)>> LocatorBindings { get; set; }
    }
}