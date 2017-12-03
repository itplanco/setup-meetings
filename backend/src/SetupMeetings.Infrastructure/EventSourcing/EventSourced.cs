using System;
using System.Collections.Generic;
using SetupMeetings.Infrastructure.Messaging;

namespace SetupMeetings.Infrastructure.EventSourcing
{
    public abstract class EventSourced : IEventSourced
    {
        private readonly Dictionary<Type, Action<IVersionedEvent>> handlers = new Dictionary<Type, Action<IVersionedEvent>>();
        private readonly List<IVersionedEvent> pendingEvents = new List<IVersionedEvent>();

        private readonly Guid _id;
        private int version = -1;

        protected EventSourced(Guid id)
        {
            _id = id;
        }

        public Guid Id
        {
            get { return _id; }
        }

        public int Version
        {
            get { return version; }
            protected set { version = value; }
        }

        public IEnumerable<IVersionedEvent> Events
        {
            get { return pendingEvents; }
        }

        protected void Handles<TEvent>(Action<TEvent> handler)
            where TEvent : IEvent
        {
            handlers.Add(typeof(TEvent), @event => handler((TEvent)@event));
        }

        protected void LoadFrom(IEnumerable<IVersionedEvent> pastEvents)
        {
            foreach (var e in pastEvents)
            {
                handlers[e.GetType()].Invoke(e);
                version = e.Version;
            }
        }

        protected void Update(VersionedEvent e)
        {
            e.SourceId = Id;
            e.Version = version + 1;
            handlers[e.GetType()].Invoke(e);
            version = e.Version;
            pendingEvents.Add(e);
        }
    }
}
