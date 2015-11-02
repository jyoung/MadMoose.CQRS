namespace MadMoose.CQRS.Specifications.Fakes
{
    using System.Threading.Tasks;
    using Commands;

    public class AnotherFakeCommandHandler : ICommandHandler<AnotherFakeCommand, Nothing>
    {
        public Task<Nothing> HandleAsync(AnotherFakeCommand command)
        {
            return Task.FromResult(Nothing.AtAll);
        }
    }
}