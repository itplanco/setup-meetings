using System;
using System.Collections.Generic;

namespace SetupMeetings.Queries.Meetings
{
    public class Meeting
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }
        public List<Organizer> Organizers { get; set; }
        public List<Attendee> Attendees { get; set; }
        public List<Invitee> Invitees { get; set; }
    }
}