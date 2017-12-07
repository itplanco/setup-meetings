using SetupMeetings.Queries.Users;
using SetupMeetings.WebApi.Infrastracture.DataStore;
using System;
using System.Linq;

namespace SetupMeetings.WebApi.Infrastracture.Queries
{
    public class UsersRepository : IUsersRepository
    {
        private SetupMeetingsQueryContext _context;

        public UsersRepository(SetupMeetingsQueryContext context)
        {
            _context = context;
        }

        public IQueryable<User> Users => _context.Users.Values.AsQueryable();

        public User FindById(Guid id)
        {
            return _context.Users.FindById(id);
        }
    }
}
