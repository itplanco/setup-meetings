using SetupMeetings.Infrastructure.Messaging;
using System;

namespace SetupMeetings.Commands.Users
{
    public class ChangeEmailAddressCommand : ICommand
    {
        public Guid Id { get; set; }
        public string NewEmailAddress { get; set; }
    }
}
