using System.ComponentModel.DataAnnotations;

namespace SetupMeetings.WebApi.Models.Meetings
{
    public class UpdatePaymentInfoRequest
    {
        [Required]
        public decimal TotalPrice { get; set; }
    }
}
