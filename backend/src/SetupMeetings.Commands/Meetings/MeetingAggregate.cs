using System;
using SetupMeetings.Infrastructure.EventSourcing;
using SetupMeetings.Common.Events.Meetings;
using System.Collections.Generic;

namespace SetupMeetings.Commands.Meetings
{
    public class MeetingAggregate : EventSourced
    {
        public MeetingAggregate(Guid id) : base(id)
        {
            Handles<MeetingCreatedEvent>(OnCreated);
        }

        public MeetingAggregate(Guid id, IEnumerable<IVersionedEvent> history) : this(id)
        {
            LoadFrom(history);
        }

        private string _name = null;
        private List<Guid> _organizers = new List<Guid>();

        public static MeetingAggregate Create(Guid meetingId, string name, Guid organizerId)
        {
            var meeting = new MeetingAggregate(meetingId);
            meeting.Update(new MeetingCreatedEvent()
            {
                SourceId = meetingId,
                Name = name,
                OrganizerId = organizerId,
            });
            return meeting;
        }

        private void OnCreated(MeetingCreatedEvent obj)
        {
            _name = obj.Name;
            _organizers.Add(obj.OrganizerId);
        }
    }
}