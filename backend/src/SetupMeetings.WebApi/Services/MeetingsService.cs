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
        Task<Guid> CreateNewMeeting(CreateMeetingCommand command);
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

        public Task<Guid> CreateNewMeeting(CreateMeetingCommand command)
        {
            command.MeetingId = Guid.NewGuid();
            _bus.Send(Envelope.Create((ICommand)command));
            return Task.FromResult(command.MeetingId);
        }
    }
}
