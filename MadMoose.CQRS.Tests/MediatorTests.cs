namespace MadMoose.CQRS.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Fakes;
    using FluentValidation;
    using Shouldly;
    using Xunit;

    public class MediatorTests : IClassFixture<ContainerFixture>
    {
        private readonly IMediator mediator;

        public MediatorTests(ContainerFixture fixture)
        {
            mediator = fixture.Container.GetInstance<IMediator>();
        }
        
        [Fact]
        public async Task When_executing_a_command_it_returns_the_result()
        {
            var response = await mediator.ExecuteAsync(new FakeCommand() { Message = "Test" });
            
            response.ShouldBe(Nothing.AtAll);
        }

        [Fact]
        public async Task When_executing_a_command_that_does_not_have_a_validator_it_returns_the_result()
        {
            var response = await mediator.ExecuteAsync(new AnotherFakeCommand());
            
            response.ShouldBe(Nothing.AtAll);
        }

        [Fact]
        public void When_executing_a_command_that_fails_validation_it_throws_an_exception()
        {
            Exception exception = null;
        
            try
            {
                mediator.ExecuteAsync(new FakeCommand()).Wait();
            }
            catch (Exception e)
            {
                exception = e.InnerException;                
            }

            exception.ShouldNotBe(null);
            exception.ShouldBeOfType<ValidationException>();
        }

        [Fact]
        public async Task When_executing_a_query_it_returns_the_result()
        {
            IList<int> response = null;

            response = await mediator.ExecuteAsync(new FakeQuery {Parameter = "Test"});

            response.Count.ShouldBe(5);
        }

        [Fact]
        public void When_executing_a_query_that_fails_validation_it_throws_an_exception()
        {
            Task<IList<int>> response = null;
            Exception exception = null;

            try
            {
                response = mediator.ExecuteAsync(new FakeQuery());
                Task.WaitAll(response);
            }
            catch (Exception e)
            {
                exception = e.InnerException;
            }
            
            exception.ShouldNotBe(null);
            exception.ShouldBeOfType<ValidationException>();
        }

        [Fact]
        public async Task When_executing_a_query_that_not_have_a_validator_it_returns_the_result()
        {
            var response = await mediator.ExecuteAsync(new AnotherFakeQuery());

            response.Count.ShouldBe(5);
        }

        [Fact]
        public async Task When_publishing_an_event_all_handlers_are_invoked()
        {
            await mediator.PublishAsync(new FakeEvent());

            var events = EventCatcher.GetEvents();
            events.Count().ShouldBe(2);

            var result = string.Join(", ", events);

            result.ShouldContain("EventHandler1");
            result.ShouldContain("EventHandler2");
        }
    }
}