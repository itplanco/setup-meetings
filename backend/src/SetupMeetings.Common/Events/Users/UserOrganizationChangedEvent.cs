using SetupMeetings.Infrastructure.EventSourcing;
using System;

namespace SetupMeetings.Common.Events.Users
{
    public class UserOrganizationChangedEvent : VersionedEvent
    {
        public Guid OldOrganizationId { get; set; }
        public Guid NewOrganizationId { get; set; }
    }
}