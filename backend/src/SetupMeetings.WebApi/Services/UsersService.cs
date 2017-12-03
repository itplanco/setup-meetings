﻿using SetupMeetings.Commands.Users;
using SetupMeetings.Infrastructure.Messaging;
using SetupMeetings.Queries;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SetupMeetings.WebApi.Services
{
    public interface IUsersService
    {
        IEnumerable<User> GetUsers();
        User GetUserById(string userId);
        Task<CreateUserCommandResult> Create(CreateUserCommand command);
        Task Process(ChangeEmailAddressCommand command);
        Task Process(ChangeOrganizationCommand command);
        Task Process(DeleteUserCommand command);
    }

    public class CreateUserCommandResult
    {
        public string UserId { get; set; }
    }

    class UsersService : IUsersService
    {
        private ICommandBus _bus;

        public UsersService(ICommandBus bus)
        {
            _bus = bus;
        }

        public User GetUserById(string userId)
        {
            return new User()
            {
                Id = "1",
                Name = "誰それ何某",
                EmailAddress = "test@example.com",
                Organization = new Organization()
                {
                    Id = "1",
                    Name = "株式会社 なんちゃら",
                }
            };
        }

        public IEnumerable<User> GetUsers()
        {
            return new[]
            {
                new User()
                {
                    Id = "1",
                    Name = "誰それ何某",
                    EmailAddress = "test@example.com",
                    Organization = new Organization()
                    {
                        Id = "1",
                        Name = "株式会社 なんちゃら",
                    }
                },
            };
        }

        public Task<CreateUserCommandResult> Create(CreateUserCommand command)
        {
            _bus.Send(Envelope.Create((ICommand)command));
            return Task.FromResult(new CreateUserCommandResult()
            {
                UserId = "1"
            });
        }

        public Task Process(ChangeEmailAddressCommand command)
        {
            return Task.CompletedTask;
        }

        public Task Process(ChangeOrganizationCommand command)
        {
            return Task.CompletedTask;
        }

        public Task Process(DeleteUserCommand command)
        {
            return Task.CompletedTask;
        }
    }
}
