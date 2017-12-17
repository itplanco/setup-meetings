using System;

namespace SetupMeetings.Commands.Meetings
{
    public class AddInviteeCommand : MeetingCommand
    {
        public Guid InviteeUserId { get; set; }
    }
}
