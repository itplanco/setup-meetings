using System.ComponentModel.DataAnnotations;

namespace SetupMeetings.WebApi.Models
{
    public class InviteeRespondToRsvpInputModel
    {
        [Required]
        public string Response { get; set; }
    }
}
