namespace MadMoose.CQRS
{
    using System.Threading.Tasks;
    using SimpleInjector;

    public class SimpleInjectorMediator : IMediator
    {
        private readonly Container container;

        public SimpleInjectorMediator(Container container)
        {
            this.container = container;
        }

        public async Task<TResponse> ExecuteAsync<TResponse>(ICommand<TResponse> command)
        {
            var commandType = command.GetType();
            var handlerType = typeof (ICommandHandler<,>).MakeGenericType(commandType, typeof (TResponse));

            var handler = (ICommandHandler<ICommand<TResponse>, TResponse>)container.GetInstance(handlerType);
            
            return await handler.Handle(command);
        }

        public async Task<TResponse> ExecuteAsync<TResponse>(IQuery<TResponse> query)
        {
            var queryType = query.GetType();
            var handlerType = typeof(ICommandHandler<,>).MakeGenericType(queryType, typeof(TResponse));

            var handler = (IQueryHandler<IQuery<TResponse>, TResponse>)container.GetInstance(handlerType);

            return await handler.Handle(query);
        }

        public async Task PublishAsync(IEvent @event)
        {
            var eventType = @event.GetType();
            var handlerType = typeof(IEventHandler<>).MakeGenericType(eventType);

            var handlers = container.GetAllInstances(handlerType);

            foreach (var handler in handlers)
            {
                await ((IEventHandler<IEvent>) handler).Handle(@event);
            }
        }
    }
}