namespace MadMoose.CQRS.Specifications
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;
    using Commands;
    using Events;
    using Fakes;
    using FluentValidation;
    using MadMoose.Specifications;
    using NUnit.Framework;
    using Queries;
    using Shouldly;
    using SimpleInjector;
    using Validation;

    public abstract class MediatorSpecificaiton : Specification
    {
        protected Container container;
        protected IMediator mediator;

        public override void Initialize()
        {
            container = new Container();

            // register objects with the container
            var assemblies = new[] {Assembly.GetExecutingAssembly()};

            container.Register<IMediator, SimpleInjectorMediator>();
            container.Register<IValidatorFactory, SimpleInjectorValidatorFactory>();
            container.Register(typeof (ICommandHandler<,>), assemblies);
            container.Register(typeof (IQueryHandler<,>), assemblies);
            container.RegisterCollection(typeof (IEventHandler<>), assemblies);

            container.Register(typeof (IValidator<>), assemblies);

            // null validators
            container.RegisterConditional(typeof (IValidator<>), typeof (NullValidator<>), c => !c.Handled);

            container.Verify();
        }

        public override void Create_subject_under_test()
        {
            mediator = container.GetInstance<IMediator>();
        }

        public class When_executing_a_command : MediatorSpecificaiton
        {
            private Task<Nothing> response;

            public override void When()
            {
                response = mediator.ExecuteAsync(new FakeCommand() {Message = "Test"});

                Task.WaitAll(response);
            }

            [Test]
            public void it_should_return_the_result()
            {
                response.Result.ShouldBe(Nothing.AtAll);
            }
        }

        public class When_executing_a_command_that_does_not_have_a_validator : MediatorSpecificaiton
        {
            private Task<Nothing> response;

            public override void When()
            {
                response = mediator.ExecuteAsync(new AnotherFakeCommand());

                Task.WaitAll(response);
            }

            [Test]
            public void it_should_return_the_result()
            {
                response.Result.ShouldBe(Nothing.AtAll);
            }
        }

        public class When_executing_a_command_that_fails_validation : MediatorSpecificaiton
        {
            private Exception exception;

            public override void When()
            {
                try
                {
                    mediator.ExecuteAsync(new FakeCommand()).Wait();
                }
                catch (Exception e)
                {
                    exception = e.InnerException;
                }
            }

            [Test]
            public void it_should_throw_a_validation_exception()
            {
                exception.ShouldNotBe(null);
                exception.ShouldBeOfType<ValidationException>();
            }
        }


        public class When_executing_a_query : MediatorSpecificaiton
        {
            private Task<IList<int>> response;

            public override void When()
            {
                response = mediator.ExecuteAsync(new FakeQuery() {Parameter = "Test"});

                Task.WaitAll(response);
            }

            [Test]
            public void it_should_return_the_result()
            {
                response.Result.Count.ShouldBe(5);
            }
        }

        public class When_executing_a_query_that_fails_validation : MediatorSpecificaiton
        {
            private Task<IList<int>> response;
            private Exception exception;

            public override void When()
            {
                try
                {
                    response = mediator.ExecuteAsync(new FakeQuery());

                    Task.WaitAll(response);
                }
                catch (Exception e)
                {
                    exception = e.InnerException;
                }
            }

            [Test]
            public void it_should_throw_a_validation_exception()
            {
                exception.ShouldNotBe(null);
                exception.ShouldBeOfType<ValidationException>();
            }
        }
    }

    public class When_executing_a_query_that_does_have_a_validator : MediatorSpecificaiton
    {
        private Task<IList<int>> response;

        public override void When()
        {
            response = mediator.ExecuteAsync(new AnotherFakeQuery());

            Task.WaitAll(response);
        }

        [Test]
        public void it_should_return_the_result()
        {
            response.Result.Count.ShouldBe(5);
        }
    }

    public class When_publising_an_event : MediatorSpecificaiton
    {
        private Task response;

        public override void When()
        {
            response = mediator.PublishAsync(new FakeEvent());

            Task.WaitAll(response);
        }

        [Test]
        public void it_should_invoke_all_the_handlers()
        {
            var events = EventCatcher.GetEvents();
            events.Count().ShouldBe(2);

            var result = string.Join(", ", events);

            result.ShouldContain("EventHandler1");
            result.ShouldContain("EventHandler2");
        }
    }
}