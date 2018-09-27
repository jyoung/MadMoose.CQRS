namespace MadMoose.CQRS.Queries
{
    /// <summary>
    /// Marker interface for Queries
    /// </summary>
    public interface IQuery
    {
        
    }

    /// <summary>
    /// Marker interface for Queryies with a response type
    /// </summary>
    /// <typeparam name="TResponse">The type of response for the query</typeparam>
    public interface IQuery<out TResponse> : IQuery
    {
         
    }
}