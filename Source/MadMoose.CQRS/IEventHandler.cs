namespace MadMoose.CQRS
{
    using System.Threading.Tasks;

    public interface IEventHandler<in TEvent> where TEvent : IEvent
    {
        Task Handle(TEvent @event);
    }
}