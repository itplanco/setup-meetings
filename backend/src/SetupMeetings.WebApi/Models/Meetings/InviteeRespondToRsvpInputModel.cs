using System.ComponentModel.DataAnnotations;

namespace SetupMeetings.WebApi.Models.Meetings
{
    public class InviteeRespondToRsvpInputModel
    {
        [Required]
        public bool Response { get; set; }
    }
}
