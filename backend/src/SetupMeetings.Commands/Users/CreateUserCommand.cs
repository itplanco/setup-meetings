using System;

namespace SetupMeetings.Commands.Users
{
    public class CreateUserCommand : UserCommand
    {
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public Guid OrganizationId { get; set; }
    }
}
