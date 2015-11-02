namespace MadMoose.CQRS.Specifications.Fakes
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Queries;

    public class AnotherFakeQueryHandler : IQueryHandler<AnotherFakeQuery, IList<int>>
    {
        public async Task<IList<int>> HandleAsync(AnotherFakeQuery query)
        {
            return await Task.FromResult(new List<int>() { 1, 2, 3, 4, 5 });
        }
    }
}