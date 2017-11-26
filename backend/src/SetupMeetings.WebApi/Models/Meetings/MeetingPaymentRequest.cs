using System.ComponentModel.DataAnnotations;

namespace SetupMeetings.WebApi.Models.Meetings
{
    public class MeetingPaymentRequest
    {
        [Required]
        public decimal TotalPrice { get; set; }
    }
}
