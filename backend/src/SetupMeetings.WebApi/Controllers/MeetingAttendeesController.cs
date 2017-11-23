using Microsoft.AspNetCore.Mvc;
using SetupMeetings.WebApi.Models;
using System.Collections.Generic;

namespace SetupMeetings.WebApi.Controllers
{
    [Route("api/meetings/{meetingId:int}/attendees")]
    public class MeetingAttendeesController : ControllerBase
    {
        [HttpGet]
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

        [HttpGet("{attendeeId:int}")]
        public IActionResult Get(int meetingsId, int attendeeId)
        {
            return Ok(new AttendeeViewModel()
            {
                UserId = 1,
                Name = "誰それ何某",
                Rsvp = false,
            });
        }

        [HttpPost]
        public IActionResult Create(int meetingId, [FromBody]AttendeeCreateCommandModel newAttendee)
        {
            return RedirectToAction("GetAttendee", new { meetingId = meetingId, attendeeId = 1 });
        }

        [HttpDelete]
        public IActionResult Delete(int meetingId, int attendeeId)
        {
            return Ok();
        }

        [HttpPut("{attendeeId:int}/rsvp")]
        public IActionResult RevpRespond(int meetingId, int attendeeId, [FromBody]string response)
        {
            return RedirectToAction("GetAttendee", new { meetingId = meetingId, attendeeId = 1 });
        }
    }
}