namespace CVB.NET.Abstractions.Ioc.Injection.Parameter
{
    using System;
    using System.Collections.Generic;

    public static class Arg<TArg>
    {
        public static TArg Dependency() => (TArg)Arg.Resolve(typeof(TArg));
    }

    public static class Arg
    {
        public static Func<Type, object> CurrentResolver => resolverStack.Peek();

        [ThreadStatic]
        private static Stack<Func<Type, object>> resolverStack;

        internal static IDisposable UseContextualResolver(Func<Type, object> resolveArgument)
        {
            if (resolverStack == null)
            {
                resolverStack = new Stack<Func<Type, object>>();
            }

            resolverStack.Push(resolveArgument);

            return new ArgContainerContext(() => resolverStack.Pop());
        }

        internal static object Resolve(Type tService)
        {
            return CurrentResolver(tService);
        }
        
        public static object Dependency(Type tService) => Resolve(tService);
    }
}