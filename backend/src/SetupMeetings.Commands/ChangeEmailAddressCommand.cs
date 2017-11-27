using SetupMeetings.Infrastructure.Messaging;
using System;

namespace SetupMeetings.Commands
{
    public class ChangeEmailAddressCommand : ICommand
    {
        public Guid Id { get; }
    }
}
