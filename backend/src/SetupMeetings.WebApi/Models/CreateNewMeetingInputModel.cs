using System;

namespace SetupMeetings.WebApi.Models
{
    public class CreateNewMeetingInputModel
    {
        public string Name { get; set; }
        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }
        public string OrganizerUserId { get; set; }
    }
}