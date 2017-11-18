using Microsoft.AspNetCore.Identity;

namespace SetupMeetings.WebApi.Models
{
    public class ApplicationUser : IdentityUser<string>
    {
        public ApplicationUser(string userName) : base(userName)
        {
        }
    }
}
