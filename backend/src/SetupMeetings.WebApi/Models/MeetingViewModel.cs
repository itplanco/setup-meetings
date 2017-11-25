using System;
using System.Collections.Generic;

namespace SetupMeetings.WebApi.Models
{
    public class MeetingViewModel
    {
        public string MeetingId { get; internal set; }
        public string Name { get; internal set; }
        public DateTime StartAt { get; internal set; }
        public DateTime EndAt { get; internal set; }
        public List<OrganizerViewModel> Organizers { get; internal set; }
        public List<InviteeViewModel> Invitees { get; internal set; }
        public List<AttendeeViewModel> Attendees { get; internal set; }
        public List<SponsorViewModel> Sponsers { get; internal set; }
    }
}