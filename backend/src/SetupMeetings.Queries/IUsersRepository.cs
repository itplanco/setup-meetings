using System.Linq;

namespace SetupMeetings.Queries
{
    public interface IUsersRepository
    {
        IQueryable<User> Users { get; }
        User FindById(string id);
    }
}
