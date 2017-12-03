using SetupMeetings.Infrastructure.Messaging;
using System;

namespace SetupMeetings.Commands.Users
{
    public class CreateUserCommand : ICommand
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public Guid OrganizationId { get; set; }
    }
}
