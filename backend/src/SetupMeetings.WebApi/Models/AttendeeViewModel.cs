namespace SetupMeetings.WebApi.Models
{
    public class AttendeeViewModel
    {
        public string Name { get; internal set; }
        public bool Rsvp { get; internal set; }
        public int UserId { get; internal set; }
    }
}