namespace MadMoose.CQRS.Commands
{
    using System.Threading.Tasks;

    /// <summary>
    /// Interface for handling commands
    /// </summary>
    /// <typeparam name="TCommand">The type of command</typeparam>
    /// <typeparam name="TResponse">The type of the response from the command</typeparam>
    public interface ICommandHandler<in TCommand, TResponse> where TCommand : ICommand<TResponse>
    {
        /// <summary>
        /// Handle the command
        /// </summary>
        /// <param name="command">The command object</param>
        /// <returns>The response of the command</returns>
        Task<TResponse> HandleAsync(TCommand command);
    }
}