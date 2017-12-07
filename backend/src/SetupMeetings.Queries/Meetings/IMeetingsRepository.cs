using System;
using System.Linq;

namespace SetupMeetings.Queries.Meetings
{
    public interface IMeetingsRepository
    {
        IQueryable<Meeting> Meetings { get; }
        Meeting FindById(Guid id);
    }
}
