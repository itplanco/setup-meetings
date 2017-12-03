using SetupMeetings.Infrastructure.Messaging;
using System;

namespace SetupMeetings.Commands.Users
{
    public abstract class UserCommand : ICommand
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
    }
}