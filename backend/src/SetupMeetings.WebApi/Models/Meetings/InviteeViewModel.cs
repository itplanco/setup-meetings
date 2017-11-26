namespace SetupMeetings.WebApi.Models.Meetings
{
    public class InviteeViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string OrganizationId { get; set; }
        public string OrganizationName { get; set; }
        public bool Rsvp { get; set; }
    }
}
