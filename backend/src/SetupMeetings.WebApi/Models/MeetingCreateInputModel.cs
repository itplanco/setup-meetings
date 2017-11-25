using System;

namespace SetupMeetings.WebApi.Controllers.Models
{
    public class MeetingCreateInputModel
    {
        public string Name { get; internal set; }
        public DateTime StartAt { get; internal set; }
        public DateTime EndAt { get; internal set; }
        public string OrganizerUserId { get; internal set; }
    }
}