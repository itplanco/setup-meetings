using System;
using System.Collections.Generic;

namespace SetupMeetings.Infrastructure.EventSourcing
{
    public interface IEventSourced
    {
        Guid Id { get; }
        int Version { get; }
        IEnumerable<IVersionedEvent> Events { get; }
        void ClearPendingEvents();
    }
}
