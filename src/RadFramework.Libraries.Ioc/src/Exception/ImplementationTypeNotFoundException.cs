using System;

using RadFramework.Abstractions.Ioc.Base.Exception;

namespace RadFramework.Libraries.Ioc.Exception
{
    internal class ImplementationTypeNotFoundException : CouldNotLocateServiceException
    {
        public ImplementationTypeNotFoundException(Type tService)
        {
        }
    }
}