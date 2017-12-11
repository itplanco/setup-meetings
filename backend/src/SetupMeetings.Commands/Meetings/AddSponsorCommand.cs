using System;

namespace SetupMeetings.Commands.Meetings
{
    public class AddSponsorCommand : MeetingCommand
    {
        public Guid SponsorUserId { get; set; }
    }
}
