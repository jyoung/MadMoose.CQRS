namespace MadMoose.CQRS.Commands
{
    /// <summary>
    /// Marker interface for commands
    /// </summary>
    public interface ICommand
    {
        
    }

    /// <summary>
    /// Marker inteface for commands with a response
    /// </summary>
    /// <typeparam name="TResponse">The type of response for the command</typeparam>
    public interface ICommand<out TResponse> : ICommand
    {
         
    }
}