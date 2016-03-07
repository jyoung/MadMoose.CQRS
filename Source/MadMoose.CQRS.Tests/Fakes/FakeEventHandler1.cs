namespace MadMoose.CQRS.Tests.Fakes
{
    using System.Threading.Tasks;
    using Events;

    public class FakeEventHandler1 : IEventHandler<FakeEvent>
    {
        public Task HandleAsync(FakeEvent @event)
        {
            EventCatcher.Catch("EventHandler1");

            return Task.FromResult(true);
        }
    }
}