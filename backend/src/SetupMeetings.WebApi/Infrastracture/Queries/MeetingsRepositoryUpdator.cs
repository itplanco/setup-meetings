using SetupMeetings.Common.Events.Meetings;
using SetupMeetings.Infrastructure.Messaging;
using SetupMeetings.Queries.Meetings;
using SetupMeetings.WebApi.Infrastracture.DataStore;
using System.Collections.Generic;

namespace SetupMeetings.WebApi.Infrastracture.Queries
{
    public class MeetingsRepositoryUpdator :
        IEventHandler<MeetingCreatedEvent>
    {
        private SetupMeetingsQueryContext _context;

        public MeetingsRepositoryUpdator(SetupMeetingsQueryContext context)
        {
            this._context = context;
        }

        public void Handle(MeetingCreatedEvent @event)
        {
            var meeting = _context.Meetings.FindById(@event.SourceId);
            if (meeting != null)
            {
                // error ?
            }

            meeting = new Meeting()
            {
                Id = @event.SourceId,
                Name = @event.Name,
                Organizers = new List<Organizer>()
                {
                    new Organizer()
                    {
                        Id = @event.OrganizerId,
                    }
                },
            };
            _context.Meetings.Add(meeting);
            _context.SaveChanges();
        }
    }
}
