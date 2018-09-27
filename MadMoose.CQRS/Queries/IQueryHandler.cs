namespace MadMoose.CQRS.Queries
{
    using System.Threading.Tasks;

    /// <summary>
    /// Inteface for implementing handling query requests
    /// </summary>
    /// <typeparam name="TQuery">The type of query to handle</typeparam>
    /// <typeparam name="TResponse">The type of response for the query</typeparam>
    public interface IQueryHandler<in TQuery, TResponse> where TQuery : IQuery<TResponse>
    {
        /// <summary>
        /// Handle the query
        /// </summary>
        /// <param name="query">The query object</param>
        /// <returns>The response of the query</returns>
        Task<TResponse> HandleAsync(TQuery query);
    }
}