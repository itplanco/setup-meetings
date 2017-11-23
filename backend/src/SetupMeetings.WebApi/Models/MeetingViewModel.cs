using System.Collections.Generic;

namespace SetupMeetings.WebApi.Models
{
    public class MeetingViewModel
    {
        public int MeetingId { get; internal set; }
        public string Name { get; internal set; }
        public List<OrganizerViewModel> Organizers { get; internal set; }
        public MeetingScheduleViewModel Schedule { get; internal set; }
        public List<AttendeeViewModel> Attendees { get; internal set; }
    }
}