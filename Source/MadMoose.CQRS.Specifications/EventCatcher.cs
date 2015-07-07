namespace MadMoose.CQRS.Specifications
{
    using System.Collections.Generic;

    public static class EventCatcher
    {
        private static readonly object lockObject = new object();

        private static readonly IList<string> events = new List<string>();

        public static void Catch(string @event)
        {
            lock (lockObject)
            {
                events.Add(@event);
            }
        }

        public static IEnumerable<string> GetEvents()
        {
            return events;
        }
    }
}