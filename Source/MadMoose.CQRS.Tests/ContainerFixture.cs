namespace MadMoose.CQRS.Tests
{
    using System;
    using System.Reflection;
    using Commands;
    using Events;
    using FluentValidation;
    using Queries;
    using SimpleInjector;
    using Validation;

    public class ContainerFixture : IDisposable
    {
        public Container Container { get; }

        public ContainerFixture()
        {
            Container = new Container();

            // register objects with the container
            var assemblies = new[] { Assembly.GetExecutingAssembly() };

            Container.Register<IMediator, SimpleInjectorMediator>();
            Container.Register<IValidatorFactory, SimpleInjectorValidatorFactory>();
            Container.Register(typeof(ICommandHandler<,>), assemblies);
            Container.Register(typeof(IQueryHandler<,>), assemblies);
            Container.RegisterCollection(typeof(IEventHandler<>), assemblies);

            Container.Register(typeof(IValidator<>), assemblies);

            // null validators
            Container.RegisterConditional(typeof(IValidator<>), typeof(NullValidator<>), c => !c.Handled);

            Container.Verify();
        }

        public void Dispose()
        {
            Container.Dispose();   
        }
    }
}