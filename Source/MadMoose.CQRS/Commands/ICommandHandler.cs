﻿namespace MadMoose.CQRS.Commands
{
    using System.Threading.Tasks;

    public interface ICommandHandler<in TCommand, TResponse> where TCommand : ICommand<TResponse>
    {
        Task<TResponse> HandleAsync(TCommand command);
    }
}