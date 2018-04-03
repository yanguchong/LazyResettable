using System.Collections.Generic;
using Unity;

namespace LazyResettable
{
    internal static class Bootstrapper
    {
        //static implementation example
        //static LazyResettable<List<Data1>> _data;

        static Bootstrapper()
        {
            //static implementation example
            //_data = new LazyResettable<List<Data1>>(() => { return new List<Data1>(); }, 1);
        }

        internal static void Init(IUnityContainer container)
        {
            //this acts like a static class
            container.RegisterType<IFileProvider, CachedFileProvider>(new Unity.Lifetime.ContainerControlledLifetimeManager());
        }
    }
}
