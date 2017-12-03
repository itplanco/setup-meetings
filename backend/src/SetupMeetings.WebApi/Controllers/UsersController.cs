﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SetupMeetings.Commands.Users;
using SetupMeetings.Queries;
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
            var user = _service.GetUserById(userId);
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
        [SwaggerOperation("createNewUser")]
        public async Task<IActionResult> CreateNewUser([FromBody]CreateNewUserRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var command = mapper.Map<CreateUserCommand>(request);
            var result = await _service.Create(command).ConfigureAwait(false);
            return CreatedAtAction(nameof(GetUser), new { userId = result.UserId }, null);
        }

        [HttpPut("{userId}/emailaddress")]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [SwaggerOperation("changeUserEmailAddress")]
        public async Task<IActionResult> ChangeEmailAddressAsync(string userId, [FromBody]ChangeEmailAddressRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var command = mapper.Map<ChangeEmailAddressCommand>(request);
            await _service.Process(command).ConfigureAwait(false);
            return AcceptedAtAction(nameof(GetUser), new { userId }, null);
        }

        [HttpPut("{userId}/organization")]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [SwaggerOperation("changeUserOrganization")]
        public async Task<IActionResult> ChangeOrganizationAsync(string userId, [FromBody]ChangeOrganizationRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var command = mapper.Map<ChangeOrganizationCommand>(request);
            await _service.Process(command).ConfigureAwait(false);
            return AcceptedAtAction(nameof(GetUser), new { userId }, null);
        }

        [HttpDelete("{userId}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [SwaggerOperation("deleteUser")]
        public async Task<IActionResult> DeleteUserAsync(string userId)
        {
            await _service.Process(new DeleteUserCommand() { UserId = new Guid(userId) }).ConfigureAwait(false);
            return NoContent();
        }
    }
}