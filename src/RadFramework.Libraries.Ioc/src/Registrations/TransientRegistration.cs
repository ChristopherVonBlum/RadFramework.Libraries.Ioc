using System;
using System.Linq;
using CVB.NET.Abstractions.Ioc.Injection.Lambda;
using CVB.NET.Abstractions.Ioc.Injection.Parameter;
using RadFramework.Libraries.Ioc.Base;
using RadFramework.Libraries.Ioc.Container;
using RadFramework.Libraries.Reflection.Caching;
using RadFramework.Libraries.Reflection.Caching.Queries;

namespace RadFramework.Libraries.Ioc.Registrations
{
    public class TransientRegistration : RegistrationBase
    {
        private readonly Container.Container container;

        private readonly Func<object> construct;
        
        public TransientRegistration(CachedType tImplementation,
            IDependencyInjectionLambdaGenerator lambdaGenerator, Container.Container container)
        {
            this.container = container;

            this.construct = lambdaGenerator.CreateConstructorInjectionLambda(
                tImplementation.Query(t =>
                    t.GetConstructors()
                        .OrderByDescending(c => ((CachedConstructorInfo) c).Query(MethodBaseQueries.GetParameters).Length)
                        .First()), 
                info => info.InnerMetaData.Name);
        }

        public override object ResolveService()
        {
            using (Arg.UseContextualResolver(container.Resolve))
            {
                return construct();
            }
        }
    }
}