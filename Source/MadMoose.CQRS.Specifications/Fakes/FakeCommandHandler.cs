namespace MadMoose.CQRS.Specifications.Fakes
{
    using System.Threading.Tasks;
    using Commands;

    public class FakeCommandHandler : ICommandHandler<FakeCommand, Nothing>
    {
        public async Task<Nothing> HandleAsync(FakeCommand command)
        {
            return await Task.FromResult(Nothing.AtAll);
        }
    }
}