using System;

namespace SetupMeetings.Commands.Users
{
    public class ChangeOrganizationCommand : UserCommand
    {
        public Guid NewOrganizationId { get; set; }
    }
}
