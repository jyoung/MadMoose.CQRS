namespace MadMoose.CQRS.Specifications
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;
    using MadMoose.Specifications;
    using NUnit.Framework;
    using Shouldly;
    using SimpleInjector;
    using SimpleInjector.Extensions;

    public abstract class MediatorSpecificaiton : Specification
    {
        protected Container container;
        protected IMediator mediator;

        public override void Initialize()
        {
            container = new Container();

            // register objects with the container
            var assembly = Assembly.GetExecutingAssembly();

            container.Register<IMediator, SimpleInjectorMediator>();
            container.RegisterManyForOpenGeneric(typeof(ICommandHandler<,>), assembly);
            container.RegisterManyForOpenGeneric(typeof(IQueryHandler<,>), assembly);
            container.RegisterManyForOpenGeneric(typeof(IEventHandler<>), container.RegisterAll, assembly);

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
                response = mediator.ExecuteAsync(new FakeCommand());

                Task.WaitAll(response);
            }

            [Test]
            public void it_should_return_the_result()
            {
                response.Result.ShouldBe(Nothing.AtAll);
            }
        }

        public class When_executing_a_query : MediatorSpecificaiton
        {
            private Task<IList<int>> response;

            public override void When()
            {
                response = mediator.ExecuteAsync(new FakeQuery());

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
}