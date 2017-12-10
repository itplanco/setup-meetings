using System;

namespace SetupMeetings.Commands.Meetings
{
    public class CreateMeetingCommand : MeetingCommand
    {
        public string Name { get; set; }
        public Guid OrganizerId { get; set; }
    }
}
