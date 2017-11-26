using System.Linq;
using SetupMeetings.Queries;
using System.Collections.Generic;
using System;

namespace SetupMeetings.Infrastructure.Databases.Xml
{
    class XmlUsersRepository : XmlRepository<User, string>, IUsersRepository
    {
        protected XmlUsersRepository(string filePath) : base(filePath, u => u.Id)
        {
        }

        public IQueryable<User> Users => Queryable;
    }
}
