using SetupMeetings.Infrastructure.EventSourcing;
using SetupMeetings.Infrastructure.Messaging;

namespace SetupMeetings.Commands.Users
{
    public class UserCommandHandler : 
        ICommandHandler<CreateUserCommand>,
        ICommandHandler<ChangeEmailAddressCommand>,
        ICommandHandler<ChangeOrganizationCommand>,
        ICommandHandler<DeleteUserCommand>
    {
        private readonly IEventSourcedRepository<UserAggregate> _repository;

        public UserCommandHandler(IEventSourcedRepository<UserAggregate> repository)
        {
            _repository = repository;
        }

        public void Handle(CreateUserCommand command)
        {
            var user = UserAggregate.CreateUser(command.UserId, command.Name, command.EmailAddress, command.OrganizationId);
            _repository.Save(user, command.Id);
        }

        public void Handle(ChangeEmailAddressCommand command)
        {
            var user = _repository.Get(command.UserId);
            user.ChangeEmailAddress(command.NewEmailAddress);
            _repository.Save(user, command.Id);
        }

        public void Handle(ChangeOrganizationCommand command)
        {
            var user = _repository.Get(command.UserId);
            user.ChangeOrganization(command.NewOrganizationId);
            _repository.Save(user, command.Id);
        }

        public void Handle(DeleteUserCommand command)
        {
            var user = _repository.Get(command.UserId);
            user.Delete();
            _repository.Save(user, command.Id);
        }
    }
}
