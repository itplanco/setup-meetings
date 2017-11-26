using Microsoft.AspNetCore.Mvc;
using SetupMeetings.WebApi.Models.Meetings;
using System;
using System.Collections.Generic;
using System.Net;

namespace SetupMeetings.WebApi.Controllers
{
    [Route("api/meetings")]
    public class MeetingsController : ControllerBase
    {
        [HttpGet("{meetingId}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(MeetingResponse))]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public IActionResult GetMeeting(string meetingId)
        {
            return Ok(new MeetingResponse()
            {
                MeetingId = meetingId,
                Name = "忘年会",
                StartAt = new DateTime(2017, 12, 7, 10, 30, 0, DateTimeKind.Utc),
                EndAt = new DateTime(2017, 12, 7, 12, 30, 0, DateTimeKind.Utc),
                Organizers = new List<OrganizerResponse>() {
                    new OrganizerResponse()
                    {
                        UserId = "1",
                        UserName = "誰それ何某",
                        OrganizationId = "1",
                        OrganizationName = "株式会社 なんちゃら",
                    },
                },
                Attendees = new List<AttendeeResponse>()
                {
                    new AttendeeResponse()
                    {
                        UserId = "1",
                        UserName = "誰それ何某",
                        OrganizationId = "1",
                        OrganizationName = "株式会社 なんちゃら",
                        Attend = false
                    }
                },
                Invitees = new List<InviteeResponse>()
                {
                    new InviteeResponse()
                    {
                        UserId = "1",
                        UserName = "誰それ何某",
                        OrganizationId = "1",
                        OrganizationName = "株式会社 なんちゃら",
                        Rsvp = false
                    }
                },
            });
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult Create([FromBody] CreateNewMeetingRequest newMeeting)
        {
            return CreatedAtAction(nameof(GetMeeting), new { meetingId = "1" });
        }

        [HttpGet("{meetingId}/sponsors")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(SponsorsResponse))]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public IActionResult GetSponsors(string meetingId)
        {
            return Ok(new SponsorsResponse()
            {
                Sponsors = new List<SponsorResponse>()
                {
                    new SponsorResponse()
                    {
                        UserId = "1",
                        UserName = "誰それ何某",
                        OrganizationId = "1",
                        OrganizationName = "株式会社 なんちゃら",
                    },
                }
            });
        }

        [HttpGet("{meetingId}/sponsors/{userId}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(SponsorResponse))]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public IActionResult GetSponsor(string meetingId, string userId)
        {
            return Ok(new SponsorResponse()
            {
                UserId = "1",
                UserName = "誰それ何某",
                OrganizationId = "1",
                OrganizationName = "株式会社 なんちゃら",
            });
        }

        [HttpPost("{meetingId}/sponsors")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult AddSponsor(string meetingId, [FromBody]CreateNewSponsorRequest newInvitee)
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

        [HttpGet("{meetingId}/invitees")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(InviteesResponse))]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public IActionResult GetInvitees(string meetingId)
        {
            return Ok(new InviteesResponse()
            {
                Invitees = new List<InviteeResponse>()
                {
                    new InviteeResponse()
                    {
                        UserId = "1",
                        UserName = "誰それ何某",
                        OrganizationId = "1",
                        OrganizationName = "株式会社 なんちゃら",
                        Rsvp = false,
                    },
                }
            });
        }

        [HttpGet("{meetingId}/invitees/{userId}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(InviteeResponse))]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public IActionResult GetInvitee(string meetingId, string userId)
        {
            return Ok(new InviteeResponse()
            {
                UserId = "1",
                UserName = "誰それ何某",
                OrganizationId = "1",
                OrganizationName = "株式会社 なんちゃら",
                Rsvp = false,
            });
        }

        [HttpPost("{meetingId}/invitees")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult AddInvitee(string meetingId, [FromBody]CreateNewInviteeRequest newInvitee)
        {
            return CreatedAtAction(nameof(GetInvitee), new { meetingId, userId = "1" });
        }

        [HttpDelete("{meetingId}/invitees/{userId}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult DeleteInvitee(string meetingId, int userId)
        {
            return Ok();
        }

        [HttpPut("{meetingId}/invitees/{userId}/rsvp")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult InviteeRespondToRsvp(string meetingId, string userId, [FromBody]InviteeRespondToRsvpRequest response)
        {
            return RedirectToAction(nameof(GetInvitee), new { meetingId, userId });
        }

        [HttpGet("{meetingId}/attendees")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AttendeesResponse))]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public IActionResult GetAttendees(string meetingId)
        {
            return Ok(new AttendeesResponse()
            {
                Attendees = new List<AttendeeResponse>()
                {
                    new AttendeeResponse()
                    {
                        UserId = "1",
                        UserName = "誰それ何某",
                        OrganizationId = "1",
                        OrganizationName = "株式会社 なんちゃら",
                        Attend = false,
                    },
                }
            });
        }

        [HttpGet("{meetingId}/attendees/{userId}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AttendeeResponse))]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public IActionResult GetAttendee(int meetingId, int userId)
        {
            return Ok(new AttendeeResponse()
            {
                UserId = "1",
                UserName = "誰それ何某",
                OrganizationId = "1",
                OrganizationName = "株式会社 なんちゃら",
                Attend = false,
            });
        }

        [HttpPost("{meetingId}/attendees")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult AddAttendee(string meetingId, [FromBody]CreateNewAttendeeRequest newAttendee)
        {
            return CreatedAtAction(nameof(GetAttendee), new { meetingId = meetingId, userId = 1 });
        }

        [HttpDelete("{meetingId}/attendees/{userId}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult RemoveAteendee(string meetingId, string userId)
        {
            return Ok();
        }

        [HttpGet("{meetingId}/payment")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(MeetingPaymentResponse))]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public IActionResult GetPaymentInfo(string meetingId)
        {
            return Ok(new MeetingPaymentResponse()
            {
                TotalPrice = 10000,
                Details = new List<MeetingPaymentDetailResponse>()
                {
                    new MeetingPaymentDetailResponse()
                    {
                        UserId = "1",
                        UserName = "誰それ何某",
                        OrganizationId = "1",
                        OrganizationName = "株式会社 なんちゃら",
                        Price = 2000,
                    },
                },
            });
        }

        [HttpPut("{meetingId}/payment")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult UpdateTotalPayment(string meetingId, [FromBody] MeetingPaymentRequest payment)
        {
            return NoContent();
        }
    }
}