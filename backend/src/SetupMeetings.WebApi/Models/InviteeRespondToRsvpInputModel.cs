using System.ComponentModel.DataAnnotations;

namespace SetupMeetings.WebApi.Models
{
    public class InviteeRespondToRsvpInputModel
    {
        [Required]
        public bool Response { get; set; }
    }
}
