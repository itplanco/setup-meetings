using System.Linq;
using SetupMeetings.Queries.Meetings;
using System.Collections.Generic;
using SetupMeetings.Queries.Common;
using System;

namespace SetupMeetings.WebApi.Infrastracture.Queries
{
    public class MeetingsRepository : IMeetingsRepository
    {
        public IQueryable<Meeting> Meetings => new EnumerableQuery<Meeting>(new List<Meeting>()
        {
            new Meeting()
            {
                Id = Guid.NewGuid(),
                Name = "忘年会",
                StartAt = new DateTime(2017, 12, 7, 10, 30, 0, DateTimeKind.Utc),
                EndAt = new DateTime(2017, 12, 7, 12, 30, 0, DateTimeKind.Utc),
                Organizers = new List<Organizer>() {
                    new Organizer()
                    {
                        Id = Guid.NewGuid(),
                        Name = "誰それ何某",
                        Organization = Organization.FindById(Guid.NewGuid()),
                    }
                },
                Sponsors = new List<Sponsor>()
                {
                    new Sponsor()
                    {
                        Id = Guid.NewGuid(),
                        Name = "誰それ何某",
                        Organization = Organization.FindById(Guid.NewGuid()),
                    },
                },
                Attendees = new List<Attendee>()
                {
                    new Attendee()
                    {
                        Id = Guid.NewGuid(),
                        Name = "誰それ何某",
                        Organization = Organization.FindById(Guid.NewGuid()),
                        Attend = false
                    }
                },
                Invitees = new List<Invitee>()
                {
                    new Invitee()
                    {
                        Id = Guid.NewGuid(),
                        Name = "誰それ何某",
                        Organization = Organization.FindById(Guid.NewGuid()),
                        Rsvp = false
                    }
                },
            },
        });

        public Meeting FindById(Guid id)
        {
            return Meetings.First();
        }
    }
}
