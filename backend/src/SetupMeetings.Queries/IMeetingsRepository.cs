using System.Linq;

namespace SetupMeetings.Queries
{
    public interface IMeetingsRepository
    {
        IQueryable<Meeting> Meetings { get; }
        Meeting FindById(string id);
    }
}
