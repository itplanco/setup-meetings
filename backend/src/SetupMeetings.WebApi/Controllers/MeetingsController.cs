using Microsoft.AspNetCore.Mvc;
using SetupMeetings.WebApi.Models;
using System;
using System.Collections.Generic;

namespace SetupMeetings.WebApi.Controllers
{
    [Route("api/meetings")]
    public class MeetingsController : ControllerBase
    {
        [HttpGet("{meetingId:int}")]
        public IActionResult GetMeeting(int meetingId)
        {
            if (meetingId != 1)
            {
                return NotFound();
            }

            return Ok(new MeetingViewModel()
            {
                MeetingId = meetingId,
                Name = "忘年会",
                Schedule = new MeetingScheduleViewModel()
                {
                    StartAt = new DateTime(2017, 12, 7, 10, 30, 0, DateTimeKind.Utc),
                    EndAt = new DateTime(2017, 12, 7, 12, 30, 0, DateTimeKind.Utc),
                },
                Organizers = new List<OrganizerViewModel>() {
                    new OrganizerViewModel()
                    {
                        UserId = 1,
                        Name = "誰それ何某",
                    },
                },
                Attendees = new List<AttendeeViewModel>()
                {
                    new AttendeeViewModel()
                    {
                        UserId = 1,
                        Name = "誰それ何某",
                        Rsvp = false
                    }
                },
            });
        }
        [HttpGet("{meetingId:int}/attendees")]
        public IActionResult GetAll(int meetingId)
        {
            if (meetingId != 1)
            {
                return NotFound();
            }

            return Ok(new
            {
                Attendees = new List<AttendeeViewModel>()
                {
                    new AttendeeViewModel()
                    {
                        UserId = 1,
                        Name = "誰それ何某",
                        Rsvp = false,
                    },
                }
            });
        }

        [HttpGet("{meetingId:int}/attendees/{attendeeId:int}")]
        public IActionResult Get(int meetingsId, int attendeeId)
        {
            return Ok(new AttendeeViewModel()
            {
                UserId = 1,
                Name = "誰それ何某",
                Rsvp = false,
            });
        }

        [HttpPost("{meetingId:int}/attendees")]
        public IActionResult Create(int meetingId, [FromBody]AttendeeCreateCommandModel newAttendee)
        {
            return RedirectToAction("GetAttendee", new { meetingId = meetingId, attendeeId = 1 });
        }

        [HttpDelete("{meetingId:int}/attendees")]
        public IActionResult Delete(int meetingId, int attendeeId)
        {
            return Ok();
        }

        [HttpPut("{meetingId:int}/attendees/{attendeeId:int}/rsvp")]
        public IActionResult RevpRespond(int meetingId, int attendeeId, [FromBody]string response)
        {
            return RedirectToAction("GetAttendee", new { meetingId = meetingId, attendeeId = 1 });
        }
    }
}