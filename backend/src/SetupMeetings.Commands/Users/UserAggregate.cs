using SetupMeetings.Common.Events.Users;
using SetupMeetings.Infrastructure.EventSourcing;
using System;
using System.Collections.Generic;

namespace SetupMeetings.Commands.Users
{
    public class UserAggregate : EventSourced
    {
        private string _name;
        private string _emailAddress;
        private Guid _organizationId;
        private bool _isDeleted;

        public UserAggregate(Guid id) : base(id)
        {
            Handles<UserCreatedEvent>(OnCreated);
            Handles<UserEmailAddressChangedEvent>(OnEmailAddressChanged);
            Handles<UserOrganizationChangedEvent>(OnOrganizationChanged);
            Handles<UserDeletedEvent>(OnDeleted);
        }

        public string Name { get { return _name; } }
        public string EmailAddress { get { return _emailAddress; } }
        public Guid OrganizationId { get { return _organizationId; } }
        public bool IsDeleted { get { return _isDeleted; } }

        public UserAggregate(Guid id, IEnumerable<IVersionedEvent> history) : this(id)
        {
            LoadFrom(history);
        }

        public static UserAggregate CreateUser(string name, string emailAddress, Guid organizationId)
        {
            var user = new UserAggregate(Guid.NewGuid());
            user.Update(new UserCreatedEvent()
            {
                Name = name,
                EmailAddress = emailAddress,
                OrganizationId = organizationId,
            });
            return user;
        }

        public void ChangeEmailAddress(string newEmailAddress)
        {
            Update(new UserEmailAddressChangedEvent { OldEmailAddress = EmailAddress, NewEmailAddress = newEmailAddress });
        }

        public void ChangeOrganization(Guid organizationId)
        {
            Update(new UserOrganizationChangedEvent { OldOrganizationId = _organizationId, NewOrganizationId = organizationId });
        }

        public void Delete()
        {
            Update(new UserDeletedEvent());
        }

        private void OnCreated(UserCreatedEvent e)
        {
            _name = e.Name;
            _emailAddress = e.EmailAddress;
            _organizationId = e.OrganizationId;
        }

        private void OnEmailAddressChanged(UserEmailAddressChangedEvent e)
        {
            _emailAddress = e.NewEmailAddress;
        }

        private void OnOrganizationChanged(UserOrganizationChangedEvent e)
        {
            _organizationId = e.NewOrganizationId;
        }

        private void OnDeleted(UserDeletedEvent obj)
        {
            _isDeleted = true;
        }
    }
}
