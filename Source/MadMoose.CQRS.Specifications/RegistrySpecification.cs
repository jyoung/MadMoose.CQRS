namespace MadMoose.CQRS.Specifications
{
    using System.Reflection;
    using Commands;
    using Events;
    using FluentValidation;
    using MadMoose.Specifications;
    using NUnit.Framework;
    using Queries;
    using SimpleInjector;
    using Validation;

    public abstract class RegistrySpecification : Specification
    {
        protected Container container;
        protected Assembly[] assemblies;

        public override void Initialize()
        {
            container = new Container();

            // register objects with the container
            assemblies = new[] { Assembly.GetExecutingAssembly() };
        }
    }

    public class When_registering_the_components : RegistrySpecification
    {
        public override void When()
        {
            CQRSRegistry.Register(container, assemblies);
        }

        [Test]
        public void it_should_verify()
        {
            container.Verify();
        }
    }
}