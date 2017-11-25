using Microsoft.AspNetCore.Mvc;
using SetupMeetings.WebApi.Models;
using System;
using System.Collections.Generic;

namespace SetupMeetings.WebApi.Controllers
{
    [Route("api/meetings")]
    public class MeetingsController : ControllerBase
    {
        [HttpGet("{meetingId}")]
        public IActionResult GetMeeting(string meetingId)
        {
            if (meetingId != "1")
            {
                return NotFound();
            }

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
        public IActionResult GetInvitees(string meetingId)
        {
            if (meetingId != "1")
            {
                return NotFound();
            }

            return Ok(new
            {
                Invitee = new List<InviteeViewModel>()
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
        public IActionResult GetInvitee(string meetingId, string inviteeId)
        {
            if (meetingId != "1" && inviteeId != "1")
            {
                return NotFound();
            }

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
        public IActionResult AddInvitee(string meetingId, [FromBody]AttendeeCreateCommandModel newAttendee)
        {
            return RedirectToAction(nameof(GetInvitee), new { meetingId, inviteeId = "1" });
        }

        [HttpDelete("{meetingId}/invitees/{inviteeId}")]
        public IActionResult DeleteInvitee(string meetingId, int inviteeId)
        {
            return Ok();
        }

        [HttpPut("{meetingId}/invitees/{inviteeId}/rsvp")]
        public IActionResult InviteeRespondToRsvp(string meetingId, string inviteeId, [FromBody]InviteeRespondToRsvpCommandModel response)
        {
            return RedirectToAction(nameof(GetInvitee), new { meetingId, inviteeId });
        }

        [HttpGet("{meetingId}/attendees")]
        public IActionResult GetAttendees(string meetingId)
        {
            if (meetingId != "1")
            {
                return NotFound();
            }

            return Ok(new
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
        public IActionResult AddAttendee(string meetingId, [FromBody]AttendeeCreateCommandModel newAttendee)
        {
            return RedirectToAction(nameof(GetAttendee), new { meetingId = meetingId, attendeeId = 1 });
        }

        [HttpDelete("{meetingId}/attendees/{attendeeId}")]
        public IActionResult RemoveAteendee(string meetingId, string attendeeId, [FromBody]AttendeeCreateCommandModel newAttendee)
        {
            return Ok();
        }
    }
}