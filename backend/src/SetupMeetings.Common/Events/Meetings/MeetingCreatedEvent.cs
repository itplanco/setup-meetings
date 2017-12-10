using System;
using SetupMeetings.Infrastructure.EventSourcing;

namespace SetupMeetings.Common.Events.Meetings
{
    public class MeetingCreatedEvent : VersionedEvent
    {
        public string Name { get; set; }
        public Guid OrganizerId { get; set; }
    }
}
