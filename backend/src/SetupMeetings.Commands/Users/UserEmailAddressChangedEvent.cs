using SetupMeetings.Infrastructure.EventSourcing;
using System;

namespace SetupMeetings.Commands.Users
{
    public class UserEmailAddressChangedEvent : VersionedEvent
    {
        public string OldEmailAddress { get; set; }
        public string NewEmailAddress { get; set; }
    }
}