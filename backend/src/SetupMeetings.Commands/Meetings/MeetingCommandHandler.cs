using SetupMeetings.Infrastructure.EventSourcing;
using SetupMeetings.Infrastructure.Messaging;

namespace SetupMeetings.Commands.Meetings
{
    public class MeetingCommandHandler :
        ICommandHandler<CreateMeetingCommand>,
        ICommandHandler<AddSponsorCommand>,
        ICommandHandler<AddInviteeCommand>,
        ICommandHandler<InviteeRespondToRsvpCommand>

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

        public void Handle(AddInviteeCommand command)
        {
            var meeting = _repository.Get(command.MeetingId);
            meeting.AddInvitee(command.InviteeUserId);
            _repository.Save(meeting, command.Id);
        }

        public void Handle(InviteeRespondToRsvpCommand command)
        {
            var meeting = _repository.Get(command.MeetingId);
            meeting.InviteeRespondToRsvp(command.InviteeUserId, command.Rsvp);
            _repository.Save(meeting, command.Id);
        }
    }
}
