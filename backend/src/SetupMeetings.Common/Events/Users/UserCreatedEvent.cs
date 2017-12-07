using SetupMeetings.Infrastructure.EventSourcing;
using System;

namespace SetupMeetings.Common.Events.Users
{
    public class UserCreatedEvent : VersionedEvent
    {
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public Guid OrganizationId { get; set; }
    }
}