using System;

namespace SetupMeetings.Infrastructure.Messaging
{
    public interface IEvent
    {
        Guid Id { get; }
    }
}