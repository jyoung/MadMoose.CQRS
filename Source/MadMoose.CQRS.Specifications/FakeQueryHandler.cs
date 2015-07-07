﻿namespace MadMoose.CQRS.Specifications
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class FakeQueryHandler : IQueryHandler<FakeQuery, IList<int>>
    {
        public async Task<IList<int>> Handle(FakeQuery query)
        {
            return await Task.FromResult(new List<int>() {1, 2, 3, 4, 5});
        }
    }
}