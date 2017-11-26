using Microsoft.AspNetCore.Mvc;
using SetupMeetings.WebApi.Models.Users;
using System;
using System.Collections.Generic;
using System.Net;

namespace SetupMeetings.WebApi.Controllers
{
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(UsersResponse))]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public IActionResult GetSponsors(string meetingId)
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
        public IActionResult GetSponsor(string meetingId, string userId)
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

        [HttpPost("{meetingId}/sponsors")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult AddSponsor(string meetingId, [FromBody]CreateNewUserRequest newInvitee)
        {
            return CreatedAtAction(nameof(GetSponsor), new { meetingId, userId = "1" });
        }

        [HttpDelete("{meetingId}/sponsors/{userId}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult DeleteSponsor(string meetingId, int userId)
        {
            return Ok();
        }
    }
}