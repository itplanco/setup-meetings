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
    }
}
