namespace SetupMeetings.WebApi.Models
{
    public class InviteeViewModel
    {
        public string UserId { get; internal set; }
        public string UserName { get; internal set; }
        public string OrganizationId { get; internal set; }
        public string OrganizationName { get; internal set; }
        public bool Rsvp { get; internal set; }
    }
}
