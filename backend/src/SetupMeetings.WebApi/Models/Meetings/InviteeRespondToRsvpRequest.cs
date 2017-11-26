using System.ComponentModel.DataAnnotations;

namespace SetupMeetings.WebApi.Models.Meetings
{
    public class InviteeRespondToRsvpRequest
    {
        [Required]
        public bool Response { get; set; }
    }
}
