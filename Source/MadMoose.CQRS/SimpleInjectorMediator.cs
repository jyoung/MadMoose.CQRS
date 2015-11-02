namespace MadMoose.CQRS
{
    using System.Threading.Tasks;
    using FluentValidation.Results;
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
            var validatorType = typeof (ICommandValidator<>).MakeGenericType(commandType);

            dynamic validator = container.GetInstance(validatorType);

            var result = (ValidationResult) await validator.ValidateAsync((dynamic) command);

            if (result.IsValid == false)
            {
                throw new ValidationException(result);
            }

            dynamic handler = container.GetInstance(handlerType);

            return await handler.HandleAsync((dynamic) command);
        }

        public async Task<TResponse> ExecuteAsync<TResponse>(IQuery<TResponse> query)
        {
            var queryType = query.GetType();
            var handlerType = typeof (IQueryHandler<,>).MakeGenericType(queryType, typeof (TResponse));
            var validatorType = typeof (ICommandValidator<>).MakeGenericType(queryType);

            dynamic validator = container.GetInstance(validatorType);

            var result = (ValidationResult) await validator.ValidateAsync((dynamic) query);

            if (result.IsValid == false)
            {
                throw new ValidationException(result);
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