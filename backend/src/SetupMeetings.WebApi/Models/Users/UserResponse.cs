using AutoMapper.Configuration.Conventions;

namespace SetupMeetings.WebApi.Models.Users
{
    public class UserResponse
    {
        [MapTo("Id")]
        public string UserId { get; set; }
        [MapTo("Name")]
        public string UserName { get; set; }
        public string EmailAddress { get; set; }
        public string OrganizationId { get; set; }
        public string OrganizationName { get; set; }
    }
}