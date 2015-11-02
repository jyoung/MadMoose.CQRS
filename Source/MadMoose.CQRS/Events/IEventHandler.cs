namespace MadMoose.CQRS.Events
{
    using System.Threading.Tasks;

    /// <summary>
    /// Interface for handling events
    /// </summary>
    /// <typeparam name="TEvent">The type of event</typeparam>
    public interface IEventHandler<in TEvent> where TEvent : IEvent
    {
        /// <summary>
        /// Handle the event
        /// </summary>
        /// <param name="@event">The event to handle</param>
        /// <returns></returns>
        Task HandleAsync(TEvent @event);
    }
}