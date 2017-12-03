using SetupMeetings.Queries.Common;
using System;

namespace SetupMeetings.Queries.Users
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public Organization Organization { get; set; }
    }
}