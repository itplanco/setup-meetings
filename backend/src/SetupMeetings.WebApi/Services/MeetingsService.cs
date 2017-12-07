using SetupMeetings.Infrastructure.Messaging;
using SetupMeetings.Queries.Meetings;
using System;

namespace SetupMeetings.WebApi.Services
{
    public interface IMeetingsService
    {
        Meeting GetMeetingById(Guid meetingId);
    }

    public class MeetingsService : IMeetingsService
    {
        private ICommandBus _bus;
        private IEventAwaiter _eventAwaiter;
        private IMeetingsRepository _repository;

        public MeetingsService(ICommandBus bus, IEventAwaiter eventAwaiter, IMeetingsRepository repository)
        {
            _bus = bus;
            _eventAwaiter = eventAwaiter;
            _repository = repository;
        }
        public Meeting GetMeetingById(Guid meetingId)
        {
            return _repository.FindById(meetingId);
        }
    }
}
