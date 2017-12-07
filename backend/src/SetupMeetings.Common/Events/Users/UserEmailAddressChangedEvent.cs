using SetupMeetings.Infrastructure.EventSourcing;

namespace SetupMeetings.Common.Events.Users
{
    public class UserEmailAddressChangedEvent : VersionedEvent
    {
        public string OldEmailAddress { get; set; }
        public string NewEmailAddress { get; set; }
    }
}