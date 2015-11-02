namespace MadMoose.CQRS.Queries
{
    using System.Threading.Tasks;

    public interface IQueryHandler<in TQuery, TResponse> where TQuery : IQuery<TResponse>
    {
        Task<TResponse> HandleAsync(TQuery query);
    }
}