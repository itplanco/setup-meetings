using SetupMeetings.Infrastructure.Messaging;
using System;

namespace SetupMeetings.Commands.Users
{
    public class ChangeEmailAddressCommand : UserCommand
    {
        public Guid Id { get; set; }
        public string NewEmailAddress { get; set; }
    }
}
