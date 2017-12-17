using System;

namespace SetupMeetings.Commands.Meetings
{
    public class InviteeRespondToRsvpCommand : MeetingCommand
    {
        public Guid InviteeUserId { get; set; }
        public bool Rsvp { get; set; }
    }
}
