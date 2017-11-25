namespace SetupMeetings.WebApi.Models
{
    public class AttendeeViewModel
    {
        public string UserId { get; internal set; }
        public string UserName { get; internal set; }
        public string OrganizationId { get; internal set; }
        public string OrganizationName { get; internal set; }
        public bool Attend { get; internal set; }
    }
}