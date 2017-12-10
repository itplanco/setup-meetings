using SetupMeetings.Queries.Meetings;
using SetupMeetings.Queries.Users;
using System;
using System.Collections.Generic;

namespace SetupMeetings.WebApi.Infrastracture.DataStore
{
    public class SetupMeetingsQueryContext
    {
        public readonly Dictionary<Guid, User> Users = new Dictionary<Guid, User>();
        public readonly Dictionary<Guid, Meeting> Meetings = new Dictionary<Guid, Meeting>();

        internal void SaveChanges()
        {
            // TODO: should save instance.
        }
    }

    public static class SetupMeetingQueryContextExtension
    {
        public static T FindById<T>(this Dictionary<Guid, T> dictionary, Guid id)
        {
            if (dictionary.TryGetValue(id, out var value))
            {
                return value;
            }
            return default(T);
        }

        public static void Add<T>(this Dictionary<Guid, T> dictionary, T value)
        {
            var id = (Guid)typeof(T).GetProperty("Id").GetValue(value);
            dictionary.Add(id, value);
        }

        public static void Remove<T>(this Dictionary<Guid, T> dictionary, T value)
        {
            var id = (Guid)typeof(T).GetProperty("Id").GetValue(value);
            dictionary.Remove(id);
        }
    }
}
