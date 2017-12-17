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
            Handles<SponsorAddedToMeetingEvent>(OnSponsorAdded);
            Handles<InviteeAddedToMeetingEvent>(OnInviteeAdded);
        }

        public MeetingAggregate(Guid id, IEnumerable<IVersionedEvent> history) : this(id)
        {
            LoadFrom(history);
        }

        private string _name = null;
        private List<Guid> _organizers = new List<Guid>();
        private List<Guid> _sponsors = new List<Guid>();
        private List<Guid> _invitees = new List<Guid>();

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

        public void AddSponsor(Guid sponsorId)
        {
            Update(new SponsorAddedToMeetingEvent() { SponsorId = sponsorId });
        }

        internal void AddInvitee(Guid inviteeId)
        {
            Update(new InviteeAddedToMeetingEvent() { InviteeId = inviteeId });
        }

        private void OnCreated(MeetingCreatedEvent obj)
        {
            _name = obj.Name;
            _organizers.Add(obj.OrganizerId);
        }

        private void OnSponsorAdded(SponsorAddedToMeetingEvent obj)
        {
            _sponsors.Add(obj.SponsorId);
        }

        private void OnInviteeAdded(InviteeAddedToMeetingEvent obj)
        {
            _invitees.Add(obj.InviteeId);
        }
    }
}