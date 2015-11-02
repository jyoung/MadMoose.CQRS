namespace MadMoose.CQRS.Specifications
{
    using System.Threading.Tasks;

    public class FakeCommandHandler : ICommandHandler<FakeCommand, Nothing>
    {
        public async Task<Nothing> HandleAsync(FakeCommand command)
        {
            return await Task.FromResult(Nothing.AtAll);
        }
    }
}