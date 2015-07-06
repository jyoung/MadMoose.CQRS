namespace MadMoose.CQRS
{
    using System.Threading.Tasks;

    public interface IMediator
    {
        Task<TResponse> ExecuteAsync<TResponse>(ICommand<TResponse> command);
        Task<TResponse> ExecuteAsync<TResponse>(IQuery<TResponse> query);
        Task PublishAsync(IEvent @event);
    }
}