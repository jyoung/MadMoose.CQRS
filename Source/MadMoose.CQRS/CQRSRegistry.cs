namespace MadMoose.CQRS
{
    using System.Reflection;
    using SimpleInjector;
    using SimpleInjector.Extensions;

    public static class CQRSRegistry
    {
        /// <summary>
        /// Registers the CQRS objects and all handlers in the supplied assemblies
        /// </summary>
        /// <param name="container"></param>
        /// <param name="assemblies"></param>
        public static void Register(Container container, params Assembly[] assemblies)
        {
            container.Register<IMediator, SimpleInjectorMediator>();

            container.RegisterManyForOpenGeneric(typeof(IQueryHandler<,>), assemblies);
            container.RegisterManyForOpenGeneric(typeof(ICommandHandler<,>), assemblies);
            container.RegisterManyForOpenGeneric(typeof(IEventHandler<>), container.RegisterAll, assemblies);
        }
    }
}