using SetupMeetings.Infrastructure.Messaging;
using System;

namespace SetupMeetings.Commands.Meetings
{
    public class MeetingCommand : ICommand
    {
        public Guid Id { get; set; }
        public Guid MeetingId { get; set; }
    }
}
