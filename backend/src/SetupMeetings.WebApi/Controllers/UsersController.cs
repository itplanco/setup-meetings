using Microsoft.AspNetCore.Mvc;
using SetupMeetings.WebApi.Models.Users;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.Net;

namespace SetupMeetings.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(UsersResponse))]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [SwaggerOperation("getUsers")]
        public IActionResult GetAllUsers()
        {
            return Ok(new UsersResponse()
            {
                Users = new List<UserResponse>()
                {
                    new UserResponse()
                    {
                        UserId = "1",
                        UserName = "誰それ何某",
                        EmailAddress = "test@example.com",
                        OrganizationId = "1",
                        OrganizationName = "株式会社 なんちゃら",
                    },
                }
            });
        }

        [HttpGet("{userId}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(UserResponse))]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [SwaggerOperation("getUsersById")]
        public IActionResult GetUser(string userId)
        {
            return Ok(new UserResponse()
            {
                UserId = "1",
                UserName = "誰それ何某",
                EmailAddress = "test@example.com",
                OrganizationId = "1",
                OrganizationName = "株式会社 なんちゃら",
            });
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [SwaggerOperation("createNewUser")]
        public IActionResult CreateNewUser([FromBody]CreateNewUserRequest newUser)
        {
            return CreatedAtAction(nameof(GetUser), new { userId = "1" });
        }

        [HttpPut("{userId}/emailaddress")]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [SwaggerOperation("changeUserEmailAddress")]
        public IActionResult ChangeEmailAddress(string userId, [FromBody]ChangeEmailAddressRequest newEmailRequest)
        {
            return AcceptedAtAction(nameof(GetUser), new { userId = "1" });
        }

        [HttpPut("{userId}/organization")]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [SwaggerOperation("changeUserOrganization")]
        public IActionResult ChangeOrganization(string userId, [FromBody]ChangeOrganizationRequest newOrganizationRequest)
        {
            return AcceptedAtAction(nameof(GetUser), new { userId = "1" });
        }

        [HttpDelete("{userId}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [SwaggerOperation("deleteUser")]
        public IActionResult DeleteUser(string userId)
        {
            return NoContent();
        }
    }
}