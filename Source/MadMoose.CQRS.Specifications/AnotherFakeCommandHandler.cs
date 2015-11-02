using System.Threading.Tasks;

namespace MadMoose.CQRS.Specifications
{
    public class AnotherFakeCommandHandler : ICommandHandler<AnotherFakeCommand, Nothing>
    {
        public Task<Nothing> HandleAsync(AnotherFakeCommand command)
        {
            return Task.FromResult(Nothing.AtAll);
        }
    }
}