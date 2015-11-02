﻿namespace MadMoose.CQRS.Events
{
    using System.Threading.Tasks;

    public interface IEventHandler<in TEvent> where TEvent : IEvent
    {
        Task HandleAsync(TEvent @event);
    }
}