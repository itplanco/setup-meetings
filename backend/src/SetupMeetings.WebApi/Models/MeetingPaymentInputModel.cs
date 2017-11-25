using System.ComponentModel.DataAnnotations;

namespace SetupMeetings.WebApi.Models
{
    public class MeetingPaymentInputModel
    {
        [Required]
        public decimal TotalPrice { get; internal set; }
    }
}
