﻿namespace MadMoose.CQRS.Specifications
{
    using System.Threading.Tasks;

    public class FakeEventHandler2 : IEventHandler<FakeEvent>
    {
        public Task HandleAsync(FakeEvent @event)
        {
            EventCatcher.Catch("EventHandler2");

            return Task.FromResult(true);
        }
    }
}