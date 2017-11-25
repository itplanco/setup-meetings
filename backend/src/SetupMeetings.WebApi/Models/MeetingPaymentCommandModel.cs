namespace SetupMeetings.WebApi.Models
{
    public class MeetingPaymentCommandModel
    {
        public int AttendeeCount { get; internal set; }
        public decimal TotalPrice { get; internal set; }
    }
}
