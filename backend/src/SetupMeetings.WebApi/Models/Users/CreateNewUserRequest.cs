using System.ComponentModel.DataAnnotations;

namespace SetupMeetings.WebApi.Models.Users
{
    public class CreateNewUserRequest
    {
        [Required]
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public string OrganizationId { get; set; }
    }
}
