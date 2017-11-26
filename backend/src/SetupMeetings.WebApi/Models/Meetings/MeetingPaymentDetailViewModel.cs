namespace SetupMeetings.WebApi.Models.Meetings
{
    public class MeetingPaymentDetailViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string OrganizationId { get; set; }
        public string OrganizationName { get; set; }
        public decimal Price { get; set; }
    }
}
