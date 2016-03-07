namespace MadMoose.CQRS.Tests
{
    using System.Threading.Tasks;
    using Fakes;
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
        public void When_executing_a_command_it_should_return_the_result()
        {
            var response = mediator.ExecuteAsync(new FakeCommand() { Message = "Test" });
            Task.WaitAll(response);

            response.Result.ShouldBe(Nothing.AtAll);
        }

        [Fact]
        public void When_executing_a_command_that_does_not_have_a_validator_it_should_return_the_result()
        {
            var response = mediator.ExecuteAsync(new AnotherFakeCommand());
            Task.WaitAll(response);

            response.Result.ShouldBe(Nothing.AtAll);
        }
    }
}