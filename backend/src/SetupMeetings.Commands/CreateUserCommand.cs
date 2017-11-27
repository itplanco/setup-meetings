using SetupMeetings.Infrastructure.Messaging;
using System;

namespace SetupMeetings.Commands
{
    public class CreateUserCommand : ICommand
    {
        public Guid Id { get; }
    }
}
