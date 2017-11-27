using SetupMeetings.Infrastructure.Messaging;
using System;

namespace SetupMeetings.Commands
{
    public class ChangeOrganizationCommand : ICommand
    {
        public Guid Id { get; }
    }
}
