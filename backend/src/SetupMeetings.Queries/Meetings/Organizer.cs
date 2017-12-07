using System;
using SetupMeetings.Queries.Common;

namespace SetupMeetings.Queries.Meetings
{
    public class Organizer
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Organization Organization { get; set; }
    }
}
