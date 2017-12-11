using System.Linq;
using SetupMeetings.Queries.Meetings;
using System.Collections.Generic;
using SetupMeetings.Queries.Common;
using System;
using SetupMeetings.WebApi.Infrastracture.DataStore;

namespace SetupMeetings.WebApi.Infrastracture.Queries
{
    public class MeetingsRepository : IMeetingsRepository
    {
        private SetupMeetingsQueryContext _context;

        public MeetingsRepository(SetupMeetingsQueryContext context)
        {
            _context = context;
        }

        public IQueryable<Meeting> Meetings => _context.Meetings.Values.AsQueryable();

        public Meeting FindById(Guid id)
        {
            var meeting = _context.Meetings.FindById(id);
            if (meeting == null)
            {
                return null;
            }

            meeting.Organizers = meeting.Organizers
                .Select(o => _context.Users.FindById(o.Id))
                .Select(user => new Organizer()
                {
                    Id = user.Id,
                    Name = user.Name,
                    Organization = user.Organization,
                })
                .ToList();
            meeting.Sponsors = meeting.Sponsors
                .Select(o => _context.Users.FindById(o.Id))
                .Select(user => new Sponsor()
                {
                    Id = user.Id,
                    Name = user.Name,
                    Organization = user.Organization,
                })
                .ToList();
            return meeting;
        }
    }
}
