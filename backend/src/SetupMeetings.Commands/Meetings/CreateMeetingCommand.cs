using System;

namespace SetupMeetings.Commands.Meetings
{
    public class CreateMeetingCommand : MeetingCommand
    {
        public string Name { get; set; }
        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }
    }
}
