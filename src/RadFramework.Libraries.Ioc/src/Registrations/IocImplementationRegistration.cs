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
    public class IocImplementationRegistration : RegistrationBase
    {
        private readonly SimpleContainer simpleContainer;

        private readonly Func<object> construct;
        
        public IocImplementationRegistration(CachedType tImplementation,
            IDependencyInjectionLambdaGenerator lambdaGenerator, SimpleContainer simpleContainer)
        {
            this.simpleContainer = simpleContainer;

            this.construct = lambdaGenerator.CreateConstructorInjectionLambda(
                tImplementation.Query(t =>
                    t.GetConstructors()
                        .OrderByDescending(c => ((CachedConstructorInfo) c).Query(MethodBaseQueries.GetParameters).Length)
                        .First()), 
                info => info.InnerMetaData.Name);
        }

        public override object ResolveService(Type serviceKey)
        {
            using (Arg.UseContextualResolver(simpleContainer.Resolve))
            {
                return construct();
            }
        }
    }
}