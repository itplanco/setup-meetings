using System.ComponentModel.DataAnnotations;

namespace SetupMeetings.WebApi.Models.Meetings
{
    public class MeetingPaymentInputModel
    {
        [Required]
        public decimal TotalPrice { get; set; }
    }
}
