using SetupMeetings.Infrastructure.Messaging;
using System;

namespace SetupMeetings.Commands
{
    public class DeleteUserCommand : ICommand
    {
        public Guid Id { get; }
        public string UserId { get; set; }
    }
}
