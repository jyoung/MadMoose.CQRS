namespace MadMoose.CQRS
{
    using System.Threading.Tasks;

    public interface IQueryHandler<in TQuery, TResponse> where TQuery : IQuery<TResponse>
    {
        Task<TResponse> Handle(TQuery query);
    }
}