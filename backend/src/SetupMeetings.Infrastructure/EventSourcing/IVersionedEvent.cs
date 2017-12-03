using SetupMeetings.Infrastructure.Messaging;

namespace SetupMeetings.Infrastructure.EventSourcing
{
    public interface IVersionedEvent : IEvent
    {
        int Version { get; }
    }
}
