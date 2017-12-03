using System;
using System.Linq;
using SetupMeetings.Queries.Users;
using System.Collections.Generic;
using SetupMeetings.Queries.Common;

namespace SetupMeetings.WebApi.Infrastracture.Queries
{
    public class UserRepository : IUsersRepository
    {
        private List<User> _userList = new List<User>()
        {
            new User()
            {
                Id = Guid.NewGuid(),
                Name = "誰それ何某",
                EmailAddress = "test@example.com",
                Organization = new Organization()
                {
                    Id = "1",
                    Name = "株式会社 なんちゃら",
                }
            },
        };

        public IQueryable<User> Users => new EnumerableQuery<User>(_userList);

        public User FindById(Guid id)
        {
            return Users.FirstOrDefault(u => u.Id == id);
        }
    }
}
