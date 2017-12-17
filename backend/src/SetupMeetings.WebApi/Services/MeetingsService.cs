using SetupMeetings.Commands.Meetings;
using SetupMeetings.Infrastructure.Messaging;
using SetupMeetings.Queries.Meetings;
using System;
using System.Threading.Tasks;

namespace SetupMeetings.WebApi.Services
{
    public interface IMeetingsService
    {
        Meeting GetMeetingById(Guid meetingId);
        Task<Guid> CreateMeeting(CreateMeetingCommand command);
        Task AddSponsor(AddSponsorCommand command);
        Task AddInvitee(AddInviteeCommand command);
        Task UpdateInviteeRsvp(InviteeRespondToRsvpCommand command);
    }

    public class MeetingsService : IMeetingsService
    {
        private ICommandBus _bus;
        private IMeetingsRepository _repository;

        public MeetingsService(ICommandBus bus, IMeetingsRepository repository)
        {
            _bus = bus;
            _repository = repository;
        }

        public Meeting GetMeetingById(Guid meetingId)
        {
            return _repository.FindById(meetingId);
        }

        public Task<Guid> CreateMeeting(CreateMeetingCommand command)
        {
            command.MeetingId = Guid.NewGuid();
            _bus.Send(Envelope.Create((ICommand)command));
            return Task.FromResult(command.MeetingId);
        }

        public Task AddSponsor(AddSponsorCommand command)
        {
            _bus.Send(Envelope.Create((ICommand)command));
            return Task.CompletedTask;
        }

        public Task AddInvitee(AddInviteeCommand command)
        {
            _bus.Send(Envelope.Create((ICommand)command));
            return Task.CompletedTask;
        }

        public Task UpdateInviteeRsvp(InviteeRespondToRsvpCommand command)
        {
            _bus.Send(Envelope.Create((ICommand)command));
            return Task.CompletedTask;
        }
    }
}
