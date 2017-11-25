namespace SetupMeetings.WebApi.Models
{
    public class MeetingPaymentDetailViewModel
    {
        public string UserId { get; internal set; }
        public string UserName { get; internal set; }
        public string OrganizationId { get; internal set; }
        public string OrganizationName { get; internal set; }
        public decimal Price { get; internal set; }
    }
}
