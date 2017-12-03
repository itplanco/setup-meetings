using SetupMeetings.Infrastructure.EventSourcing;
using System;

namespace SetupMeetings.Commands
{
    public class UserCreatedEvent : VersionedEvent
    {
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public Guid OrganizationId { get; set; }
    }
}