namespace MadMoose.CQRS
{
    using System.Reflection;
    using Commands;
    using Events;
    using FluentValidation;
    using Queries;
    using SimpleInjector;
    using Validation;

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
            container.Register<IValidatorFactory, SimpleInjectorValidatorFactory>();

            container.Register(typeof(IQueryHandler<,>), assemblies);
            container.Register(typeof(ICommandHandler<,>), assemblies);
            container.RegisterCollection(typeof(IEventHandler<>), assemblies);

            // validators
            container.RegisterCollection(typeof(IValidator<ICommand>), assemblies);
            container.RegisterCollection(typeof(IValidator<IQuery>), assemblies);

            // null validators
            container.RegisterConditional(typeof(IValidator<ICommand>), typeof(NullValidator<ICommand>), c => !c.Handled);
            container.RegisterConditional(typeof(IValidator<IQuery>), typeof(NullValidator<IQuery>), c => !c.Handled);
        }
    }
}