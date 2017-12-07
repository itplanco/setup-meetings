using System;
using System.Linq;
using SetupMeetings.Queries.Users;
using System.Collections.Generic;
using SetupMeetings.Queries.Common;

namespace SetupMeetings.WebApi.Infrastracture.Queries
{
    public class UsersRepository : IUsersRepository
    {
        private List<User> _userList = new List<User>();

        public IQueryable<User> Users => new EnumerableQuery<User>(_userList);

        public User FindById(Guid id)
        {
            return Users.FirstOrDefault(u => u.Id == id);
        }
    }
}
