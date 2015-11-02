namespace MadMoose.CQRS.Queries
{
    public interface IQuery
    {
        
    }

    public interface IQuery<out TResponse> : IQuery
    {
         
    }
}