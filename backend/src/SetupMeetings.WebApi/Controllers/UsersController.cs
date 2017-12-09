using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SetupMeetings.Commands.Users;
using SetupMeetings.Queries.Users;
using SetupMeetings.WebApi.Models.Users;
using SetupMeetings.WebApi.Services;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace SetupMeetings.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private IUsersService _service;
        private IMapper mapper;

        public UsersController(IUsersService service)
        {
            _service = service;
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.CreateMap<User, UserResponse>();
                c.CreateMap<CreateNewUserRequest, CreateUserCommand>();
                c.CreateMap<ChangeEmailAddressRequest, ChangeEmailAddressCommand>();
                c.CreateMap<ChangeOrganizationRequest, ChangeOrganizationCommand>();
            });
            mapper = mapperConfig.CreateMapper();
        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(UsersResponse))]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [SwaggerOperation("getUsers")]
        public IActionResult GetAllUsers()
        {
            var users = _service.GetUsers();
            var response = new UsersResponse()
            {
                Users = mapper.Map<List<UserResponse>>(users),
            };
            return Ok(response);
        }

        [HttpGet("{userId}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(UserResponse))]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [SwaggerOperation("getUsersById")]
        public IActionResult GetUser(string userId)
        {
            if (!Guid.TryParse(userId, out var guidUserId))
            {
                return NotFound();
            }

            var user = _service.GetUserById(guidUserId);
            if (user == null)
            {
                return NotFound();
            }

            var response = mapper.Map<UserResponse>(user);
            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [SwaggerOperation("createUser")]
        public async Task<IActionResult> CreateUser([FromBody]CreateNewUserRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var command = mapper.Map<CreateUserCommand>(request);
            command.Id = Guid.NewGuid();
            var userId = await _service.CreateUser(command).ConfigureAwait(false);
            return CreatedAtAction(nameof(GetUser), new { userId }, null);
        }

        [HttpPut("{userId}/emailaddress")]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [SwaggerOperation("changeUserEmailAddress")]
        public async Task<IActionResult> ChangeEmailAddressAsync(string userId, [FromBody]ChangeEmailAddressRequest request)
        {
            if (!Guid.TryParse(userId, out var guidUserId))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var command = mapper.Map<ChangeEmailAddressCommand>(request);
            command.UserId = guidUserId;
            await _service.ChangeEmailAddress(command).ConfigureAwait(false);
            return AcceptedAtAction(nameof(GetUser), new { userId }, null);
        }

        [HttpPut("{userId}/organization")]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [SwaggerOperation("changeUserOrganization")]
        public async Task<IActionResult> ChangeOrganizationAsync(string userId, [FromBody]ChangeOrganizationRequest request)
        {
            if (!Guid.TryParse(userId, out var guidUserId))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var command = mapper.Map<ChangeOrganizationCommand>(request);
            command.UserId = guidUserId;
            await _service.ChangeOrganization(command).ConfigureAwait(false);
            return AcceptedAtAction(nameof(GetUser), new { userId }, null);
        }

        [HttpDelete("{userId}")]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [SwaggerOperation("deleteUser")]
        public async Task<IActionResult> DeleteUserAsync(string userId)
        {
            if (!Guid.TryParse(userId, out var guidUserId))
            {
                return NotFound();
            }

            await _service.Delete(new DeleteUserCommand() { UserId = guidUserId }).ConfigureAwait(false);
            return Accepted();
        }
    }
}