namespace MadMoose.CQRS.Specifications.Fakes
{
    using System.Threading.Tasks;
    using Events;

    public class FakeEventHandler2 : IEventHandler<FakeEvent>
    {
        public Task HandleAsync(FakeEvent @event)
        {
            EventCatcher.Catch("EventHandler2");

            return Task.FromResult(true);
        }
    }
}