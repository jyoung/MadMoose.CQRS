namespace MadMoose.CQRS.Tests.Fakes
{
    using System.Collections.Generic;
    using Queries;

    public class FakeQuery : IQuery<IList<int>>
    {
        public string Parameter { get; set; }
    }
}