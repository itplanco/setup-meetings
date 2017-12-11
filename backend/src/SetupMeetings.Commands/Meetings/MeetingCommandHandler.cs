using SetupMeetings.Infrastructure.EventSourcing;
using SetupMeetings.Infrastructure.Messaging;

namespace SetupMeetings.Commands.Meetings
{
    public class MeetingCommandHandler :
        ICommandHandler<CreateMeetingCommand>,
        ICommandHandler<AddSponsorCommand>
    {
        private IEventSourcedRepository<MeetingAggregate> _repository;

        public MeetingCommandHandler(IEventSourcedRepository<MeetingAggregate> repository)
        {
            _repository = repository;
        }

        public void Handle(CreateMeetingCommand command)
        {
            var meeting = MeetingAggregate.Create(command.MeetingId, command.Name, command.OrganizerId);
            _repository.Save(meeting, command.Id);
        }

        public void Handle(AddSponsorCommand command)
        {
            var meeting = _repository.Get(command.MeetingId);
            meeting.AddSponsor(command.SponsorUserId);
            _repository.Save(meeting, command.Id);
        }
    }
}
