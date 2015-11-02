namespace MadMoose.CQRS
{
    using System.Threading.Tasks;
    using Commands;
    using Events;
    using Queries;

    /// <summary>
    /// Primary interface for executing commands and queries as well as publish events
    /// </summary>
    public interface IMediator
    {
        /// <summary>
        /// Execute a command
        /// </summary>
        /// <typeparam name="TResponse">The response type of the command</typeparam>
        /// <param name="command">The command to execute</param>
        /// <returns>The response of the commmand</returns>
        Task<TResponse> ExecuteAsync<TResponse>(ICommand<TResponse> command);
        /// <summary>
        /// Execute a query
        /// </summary>
        /// <typeparam name="TResponse">The response type of the query</typeparam>
        /// <param name="query">The query to execute</param>
        /// <returns>The response of the query</returns>
        Task<TResponse> ExecuteAsync<TResponse>(IQuery<TResponse> query);
        /// <summary>
        /// Publishes an event
        /// </summary>
        /// <param name="@event">The event to publish</param>
        /// <returns>Nothing</returns>
        Task PublishAsync(IEvent @event);
    }
}