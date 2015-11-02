namespace MadMoose.CQRS
{
    using System.Threading.Tasks;
    using Commands;
    using Events;
    using FluentValidation;
    using Queries;
    using SimpleInjector;

    public class SimpleInjectorMediator : IMediator
    {
        private readonly Container container;
        private readonly IValidatorFactory validatorFactory;

        public SimpleInjectorMediator(Container container, IValidatorFactory validatorFactory)
        {
            this.container = container;
            this.validatorFactory = validatorFactory;
        }

        public async Task<TResponse> ExecuteAsync<TResponse>(ICommand<TResponse> command)
        {
            var commandType = command.GetType();
            var handlerType = typeof (ICommandHandler<,>).MakeGenericType(commandType, typeof (TResponse));

            var validator = validatorFactory.GetValidator(commandType);

            var result = await validator.ValidateAsync(command);
            if (result.IsValid == false)
            {
                throw new ValidationException("Command Failed Validation", result.Errors);
            }

            dynamic handler = container.GetInstance(handlerType);

            return await handler.HandleAsync((dynamic) command);
        }

        public async Task<TResponse> ExecuteAsync<TResponse>(IQuery<TResponse> query)
        {
            var queryType = query.GetType();
            var handlerType = typeof (IQueryHandler<,>).MakeGenericType(queryType, typeof (TResponse));

            var validator = validatorFactory.GetValidator(queryType);

            var result = await validator.ValidateAsync(query);
            if (result.IsValid == false)
            {
                throw new ValidationException("Command Failed Validation", result.Errors);
            }

            dynamic handler = container.GetInstance(handlerType);

            return await handler.HandleAsync((dynamic) query);
        }

        public async Task PublishAsync(IEvent @event)
        {
            var eventType = @event.GetType();
            var handlerType = typeof (IEventHandler<>).MakeGenericType(eventType);

            var handlers = container.GetAllInstances(handlerType);

            foreach (dynamic handler in handlers)
            {
                await handler.HandleAsync((dynamic) @event);
            }
        }
    }
}