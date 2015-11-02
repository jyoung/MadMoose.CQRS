namespace MadMoose.CQRS
{
    public interface ICommand
    {
        
    }

    public interface ICommand<out TResponse> : ICommand
    {
         
    }
}