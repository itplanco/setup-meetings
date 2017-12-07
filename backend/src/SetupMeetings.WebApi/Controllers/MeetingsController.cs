using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SetupMeetings.Queries.Meetings;
using SetupMeetings.WebApi.Models.Meetings;
using SetupMeetings.WebApi.Services;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Net;

namespace SetupMeetings.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/meetings")]
    public class MeetingsController : ControllerBase
    {
        private IMeetingsService _service;
        private IMapper _mapper;

        public MeetingsController(IMeetingsService service)
        {
            _service = service;
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.CreateMap<Meeting, MeetingResponse>();
            });
            _mapper = mapperConfig.CreateMapper();
        }

        [HttpGet("{meetingId}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(MeetingResponse))]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [SwaggerOperation("getMeetingById")]
        public IActionResult GetMeeting(string meetingId)
        {
            if (!Guid.TryParse(meetingId, out var guidMeetingId))
            {
                return NotFound();
            }

            var meeting = _service.GetMeetingById(guidMeetingId);
            if (meeting == null)
            {
                return NotFound();
            }
            var response = _mapper.Map<MeetingResponse>(meeting);
            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [SwaggerOperation("createNewMeeting")]
        public IActionResult CreateNewMeeting([FromBody] CreateNewMeetingRequest newMeeting)
        {
            return CreatedAtAction(nameof(GetMeeting), new { meetingId = "1" }, null);
        }

        [HttpGet("{meetingId}/sponsors")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(SponsorsResponse))]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [SwaggerOperation("getSponsors")]
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
        [SwaggerOperation("getSponsorById")]
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
        [SwaggerOperation("addSponsor")]
        public IActionResult AddSponsor(string meetingId, [FromBody]CreateNewSponsorRequest newInvitee)
        {
            return CreatedAtAction(nameof(GetSponsor), new { meetingId, userId = "1" }, null);
        }

        [HttpDelete("{meetingId}/sponsors/{userId}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [SwaggerOperation("deleteSponsor")]
        public IActionResult DeleteSponsor(string meetingId, string userId)
        {
            return NoContent();
        }

        [HttpGet("{meetingId}/invitees")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(InviteesResponse))]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [SwaggerOperation("getInvitees")]
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
        [SwaggerOperation("getInviteesById")]
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
        [SwaggerOperation("addInvitee")]
        public IActionResult AddInvitee(string meetingId, [FromBody]CreateNewInviteeRequest newInvitee)
        {
            return CreatedAtAction(nameof(GetInvitee), new { meetingId, userId = "1" }, null);
        }

        [HttpDelete("{meetingId}/invitees/{userId}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [SwaggerOperation("deleteInvitee")]
        public IActionResult DeleteInvitee(string meetingId, string userId)
        {
            return NoContent();
        }

        [HttpPut("{meetingId}/invitees/{userId}/rsvp")]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [SwaggerOperation("updateInviteeRsvp")]
        public IActionResult InviteeRespondToRsvp(string meetingId, string userId, [FromBody]InviteeRespondToRsvpRequest response)
        {
            return AcceptedAtAction(nameof(GetInvitee), new { meetingId, userId }, null);
        }

        [HttpGet("{meetingId}/attendees")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AttendeesResponse))]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [SwaggerOperation("getAttendees")]
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
        [SwaggerOperation("getAttendeesById")]
        public IActionResult GetAttendee(string meetingId, string userId)
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
        [SwaggerOperation("addAttendee")]
        public IActionResult AddAttendee(string meetingId, [FromBody]CreateNewAttendeeRequest newAttendee)
        {
            return CreatedAtAction(nameof(GetAttendee), new { meetingId, userId = 1 }, null);
        }

        [HttpPut("{meetingId}/attendees/{userId}/attendance")]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [SwaggerOperation("updateAttendeeAttendance")]
        public IActionResult UpdateAttendance(string meetingId, string userId, [FromBody] AttendanceRequest request)
        {
            return AcceptedAtAction(nameof(GetAttendee), new { meetingId, userId = 1 }, null);
        }

        [HttpPut("{meetingId}/attendees/{userId}/payment")]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [SwaggerOperation("updateAttendeePayment")]
        public IActionResult UpdatePayment(string meetingId, string userId, [FromBody] PaymentRequest request)
        {
            return AcceptedAtAction(nameof(GetAttendee), new { meetingId, userId = 1 }, null);
        }

        [HttpDelete("{meetingId}/attendees/{userId}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [SwaggerOperation("deleteAttendee")]
        public IActionResult DeleteAteendee(string meetingId, string userId)
        {
            return NoContent();
        }

        [HttpGet("{meetingId}/payment")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(MeetingPaymentResponse))]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [SwaggerOperation("getPayment")]
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
        [SwaggerOperation("updatePaymentInfo")]
        public IActionResult UpdatePaymentInfo(string meetingId, [FromBody] UpdatePaymentInfoRequest payment)
        {
            return NoContent();
        }
    }
}