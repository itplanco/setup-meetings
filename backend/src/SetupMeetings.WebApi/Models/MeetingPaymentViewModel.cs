using System.Collections.Generic;

namespace SetupMeetings.WebApi.Models
{
    public class MeetingPaymentViewModel
    {
        public decimal TotalPrice { get; set; }
        public List<MeetingPaymentDetailViewModel> Details { get; set; }
    }
}
