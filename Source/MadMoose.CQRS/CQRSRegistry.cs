namespace MadMoose.CQRS
{
    using System.Reflection;
    using SimpleInjector;

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

            container.Register(typeof(IQueryHandler<,>), assemblies);
            container.Register(typeof(ICommandHandler<,>), assemblies);
            container.RegisterCollection(typeof(IEventHandler<>), assemblies);

            // null validators
            container.RegisterConditional(typeof(ICommandValidator<>), typeof(NullCommandValidator<>), c => !c.Handled);
            container.RegisterConditional(typeof(IQueryValidator<>), typeof(NullQueryValidator<>), c => !c.Handled);
        }
    }
}