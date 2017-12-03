using SetupMeetings.Commands.Users;
using SetupMeetings.Infrastructure.Messaging;
using SetupMeetings.Queries.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SetupMeetings.WebApi.Services
{
    public interface IUsersService
    {
        IEnumerable<User> GetUsers();
        User GetUserById(Guid userId);
        Task<Guid> Create(CreateUserCommand command);
        Task ChangeEmailAddress(ChangeEmailAddressCommand command);
        Task ChangeOrganization(ChangeOrganizationCommand command);
        Task Delete(DeleteUserCommand command);
    }

    class UsersService : IUsersService
    {
        private ICommandBus _bus;
        private IEventAwaiter _eventAwaiter;
        private IUsersRepository _repository;

        public UsersService(ICommandBus bus, IEventAwaiter eventAwaiter, IUsersRepository repository)
        {
            _bus = bus;
            _eventAwaiter = eventAwaiter;
            _repository = repository;
        }

        public User GetUserById(Guid userId)
        {
            return _repository.FindById(userId);
        }

        public IEnumerable<User> GetUsers()
        {
            return _repository.Users.ToList();
        }

        public async Task<Guid> Create(CreateUserCommand command)
        {
            _bus.Send(Envelope.Create((ICommand)command));
            var result = await _eventAwaiter.WaitForMessage<UserCreatedEvent>(command.Id, TimeSpan.FromSeconds(1));
            return result.SourceId;
        }

        public Task ChangeEmailAddress(ChangeEmailAddressCommand command)
        {
            _bus.Send(Envelope.Create((ICommand)command));
            return Task.CompletedTask;
        }

        public Task ChangeOrganization(ChangeOrganizationCommand command)
        {
            _bus.Send(Envelope.Create((ICommand)command));
            return Task.CompletedTask;
        }

        public Task Delete(DeleteUserCommand command)
        {
            _bus.Send(Envelope.Create((ICommand)command));
            return Task.CompletedTask;
        }
    }
}
