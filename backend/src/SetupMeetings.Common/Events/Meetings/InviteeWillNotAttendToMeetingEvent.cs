using SetupMeetings.Infrastructure.EventSourcing;
using System;

namespace SetupMeetings.Common.Events.Meetings
{
    public class InviteeWillNotAttendToMeetingEvent : VersionedEvent
    {
        public Guid InviteeId { get; set; }
    }
}