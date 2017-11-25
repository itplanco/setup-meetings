using System.Collections.Generic;

namespace SetupMeetings.WebApi.Models
{
    public class MeetingPaymentViewModel
    {
        public decimal TotalPrice { get; internal set; }
        public List<MeetingPaymentDetailViewModel> Details { get; internal set; }
    }
}
