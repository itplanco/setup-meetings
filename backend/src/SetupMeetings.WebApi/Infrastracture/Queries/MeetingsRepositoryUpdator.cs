using SetupMeetings.Common.Events.Meetings;
using SetupMeetings.Infrastructure.Messaging;
using SetupMeetings.Queries.Meetings;
using SetupMeetings.WebApi.Infrastracture.DataStore;

namespace SetupMeetings.WebApi.Infrastracture.Queries
{
    public class MeetingsRepositoryUpdator :
        IEventHandler<MeetingCreatedEvent>,
        IEventHandler<SponsorAddedToMeetingEvent>
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
            };
            meeting.Organizers.Add(new Organizer()
            {
                Id = @event.OrganizerId,
            });
            _context.Meetings.Add(meeting);
            _context.SaveChanges();
        }

        public void Handle(SponsorAddedToMeetingEvent @event)
        {
            var meeting = _context.Meetings.FindById(@event.SourceId);
            if (meeting == null)
            {
                return;
            }

            meeting.Sponsors.Add(new Sponsor()
            {
                Id = @event.SponsorId,
            });
            _context.SaveChanges();
        }
    }
}
