using System;
using System.Collections.Generic;

namespace SetupMeetings.WebApi.Models.Meetings
{
    public class MeetingViewModel
    {
        public string MeetingId { get; set; }
        public string Name { get; set; }
        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }
        public List<OrganizerViewModel> Organizers { get; set; }
        public List<InviteeViewModel> Invitees { get; set; }
        public List<AttendeeViewModel> Attendees { get; set; }
        public List<SponsorViewModel> Sponsers { get; set; }
    }
}