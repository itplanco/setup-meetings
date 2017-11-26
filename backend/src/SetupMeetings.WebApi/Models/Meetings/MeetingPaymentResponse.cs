using System.Collections.Generic;

namespace SetupMeetings.WebApi.Models.Meetings
{
    public class MeetingPaymentResponse
    {
        public decimal TotalPrice { get; set; }
        public List<MeetingPaymentDetailResponse> Details { get; set; }
    }
}
