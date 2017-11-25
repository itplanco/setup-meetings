using Microsoft.AspNetCore.Mvc;
using SetupMeetings.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Net;

namespace SetupMeetings.WebApi.Controllers
{
    [Route("api/meetings")]
    public class MeetingsController : ControllerBase
    {
        [HttpGet("{meetingId}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(MeetingViewModel))]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public IActionResult GetMeeting(string meetingId)
        {
            return Ok(new MeetingViewModel()
            {
                MeetingId = meetingId,
                Name = "忘年会",
                StartAt = new DateTime(2017, 12, 7, 10, 30, 0, DateTimeKind.Utc),
                EndAt = new DateTime(2017, 12, 7, 12, 30, 0, DateTimeKind.Utc),
                Organizers = new List<OrganizerViewModel>() {
                    new OrganizerViewModel()
                    {
                        UserId = "1",
                        UserName = "誰それ何某",
                        OrganizationId = "1",
                        OrganizationName = "株式会社 なんちゃら",
                    },
                },
                Attendees = new List<AttendeeViewModel>()
                {
                    new AttendeeViewModel()
                    {
                        UserId = "1",
                        UserName = "誰それ何某",
                        OrganizationId = "1",
                        OrganizationName = "株式会社 なんちゃら",
                        Attend = false
                    }
                },
                Invitees = new List<InviteeViewModel>()
                {
                    new InviteeViewModel()
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

        [HttpGet("{meetingId}/invitees")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(InviteesViewModel))]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public IActionResult GetInvitees(string meetingId)
        {
            return Ok(new InviteesViewModel()
            {
                Invitees = new List<InviteeViewModel>()
                {
                    new InviteeViewModel()
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

        [HttpGet("{meetingId}/invitees/{inviteeId}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(InviteeViewModel))]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public IActionResult GetInvitee(string meetingId, string inviteeId)
        {
            return Ok(new InviteeViewModel()
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
        public IActionResult AddInvitee(string meetingId, [FromBody]AttendeeCreateInputModel newAttendee)
        {
            return CreatedAtAction(nameof(GetAttendee), new { meetingId, inviteeId = "1" });
        }

        [HttpDelete("{meetingId}/invitees/{inviteeId}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult DeleteInvitee(string meetingId, int inviteeId)
        {
            return Ok();
        }

        [HttpPut("{meetingId}/invitees/{inviteeId}/rsvp")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult InviteeRespondToRsvp(string meetingId, string inviteeId, [FromBody]InviteeRespondToRsvpInputModel response)
        {
            return RedirectToAction(nameof(GetInvitee), new { meetingId, inviteeId });
        }

        [HttpGet("{meetingId}/attendees")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AttendeesViewModel))]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public IActionResult GetAttendees(string meetingId)
        {
            return Ok(new AttendeesViewModel()
            {
                Attendees = new List<AttendeeViewModel>()
                {
                    new AttendeeViewModel()
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

        [HttpGet("{meetingId}/attendees/{attendeeId}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AttendeeViewModel))]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public IActionResult GetAttendee(int meetingId, int attendeeId)
        {
            return Ok(new AttendeeViewModel()
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
        public IActionResult AddAttendee(string meetingId, [FromBody]AttendeeCreateInputModel newAttendee)
        {
            return CreatedAtAction(nameof(GetAttendee), new { meetingId = meetingId, attendeeId = 1 });
        }

        [HttpDelete("{meetingId}/attendees/{attendeeId}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult RemoveAteendee(string meetingId, string attendeeId, [FromBody]AttendeeCreateInputModel newAttendee)
        {
            return Ok();
        }

        [HttpGet("{meetingId}/payment")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(MeetingPaymentViewModel))]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public IActionResult GetPaymentInfo(string meetingId)
        {
            return Ok(new MeetingPaymentViewModel()
            {
                TotalPrice = 10000,
                Details = new List<MeetingPaymentDetailViewModel>()
                {
                    new MeetingPaymentDetailViewModel()
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
        public IActionResult UpdateTotalPayment(string meetingId, [FromBody] MeetingPaymentInputModel payment)
        {
            return NoContent();
        }
    }
}