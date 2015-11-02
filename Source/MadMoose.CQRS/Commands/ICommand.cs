namespace MadMoose.CQRS.Commands
{
    public interface ICommand
    {
        
    }

    public interface ICommand<out TResponse> : ICommand
    {
         
    }
}