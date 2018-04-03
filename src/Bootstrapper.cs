using Unity;

namespace LazyResettable
{
    internal static class Bootstrapper
    {
        internal static void Init(IUnityContainer container)
        {
            //this acts like a static class
            container.RegisterType<IFileProvider, CachedFileProvider>(new Unity.Lifetime.ContainerControlledLifetimeManager());
        }
    }
}
