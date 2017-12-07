using SetupMeetings.Queries.Users;
using System;
using System.Collections.Generic;

namespace SetupMeetings.WebApi.Infrastracture.DataStore
{
    public class SetupMeetingsQueryContext
    {
        public readonly Dictionary<Guid, User> Users = new Dictionary<Guid, User>();

        internal void SaveChanges()
        {
            // TODO: should save instance.
        }
    }

    public static class SetupMeetingQueryContextExtension
    {
        public static User FindById(this Dictionary<Guid, User> users, Guid id)
        {
            if (users.TryGetValue(id, out var user))
            {
                return user;
            }
            return null;
        }

        public static void Add(this Dictionary<Guid, User> users, User user)
        {
            users.Add(user.Id, user);
        }

        public static void Remove(this Dictionary<Guid, User> users, User user)
        {
            users.Remove(user.Id);
        }
    }
}
