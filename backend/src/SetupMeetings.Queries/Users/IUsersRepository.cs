﻿using System;
using System.Linq;

namespace SetupMeetings.Queries.Users
{
    public interface IUsersRepository
    {
        IQueryable<User> Users { get; }
        User FindById(Guid id);
    }
}
