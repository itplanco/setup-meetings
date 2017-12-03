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
            _repository.Find(command.UserId);
        }

        public void Handle(ChangeEmailAddressCommand command)
        {
            throw new System.NotImplementedException();
        }

        public void Handle(ChangeOrganizationCommand command)
        {
            throw new System.NotImplementedException();
        }

        public void Handle(DeleteUserCommand command)
        {
            throw new System.NotImplementedException();
        }
    }
}
