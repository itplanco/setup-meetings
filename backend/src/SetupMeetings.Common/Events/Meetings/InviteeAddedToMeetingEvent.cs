using SetupMeetings.Infrastructure.EventSourcing;
using System;

namespace SetupMeetings.Common.Events.Meetings
{
    public class InviteeAddedToMeetingEvent : VersionedEvent
    {
        public Guid InviteeId { get; set; }
    }
}