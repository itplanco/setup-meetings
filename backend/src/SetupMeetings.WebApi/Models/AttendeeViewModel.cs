namespace SetupMeetings.WebApi.Models
{
    public class AttendeeViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string OrganizationId { get; set; }
        public string OrganizationName { get; set; }
        public bool Attend { get; set; }
    }
}