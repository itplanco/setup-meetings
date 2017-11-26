using System;
using System.Collections.Generic;

namespace SetupMeetings.WebApi.Models.Meetings
{
    public class MeetingResponse
    {
        public string MeetingId { get; set; }
        public string Name { get; set; }
        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }
        public List<OrganizerResponse> Organizers { get; set; }
        public List<InviteeResponse> Invitees { get; set; }
        public List<AttendeeResponse> Attendees { get; set; }
        public List<SponsorResponse> Sponsers { get; set; }
    }
}