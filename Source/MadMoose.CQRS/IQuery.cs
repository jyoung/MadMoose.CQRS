namespace MadMoose.CQRS
{
    public interface IQuery
    {
        
    }

    public interface IQuery<out TResponse> : IQuery
    {
         
    }
}