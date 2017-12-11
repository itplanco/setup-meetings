using SetupMeetings.Infrastructure.EventSourcing;
using System;

namespace SetupMeetings.Common.Events.Meetings
{
    public class SponsorAddedToMeetingEvent : VersionedEvent
    {
        public Guid SponsorId { get; set; }
    }
}