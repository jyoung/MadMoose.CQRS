namespace MadMoose.CQRS.Specifications
{
    using System.Threading.Tasks;

    public class FakeEventHandler1 : IEventHandler<FakeEvent>
    {
        public Task Handle(FakeEvent @event)
        {
            EventCatcher.Catch("EventHandler1");

            return Task.FromResult(true);
        }
    }
}