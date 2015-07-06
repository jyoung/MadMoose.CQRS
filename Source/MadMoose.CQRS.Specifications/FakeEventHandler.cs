namespace MadMoose.CQRS.Specifications
{
    using System.Threading.Tasks;

    public class FakeEventHandler : IEventHandler<FakeEvent>
    {
        public Task Handle(FakeEvent @event)
        {
            return Task.FromResult(true);
        }
    }
}