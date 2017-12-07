using SetupMeetings.Common.Events.Users;
using SetupMeetings.Infrastructure.Messaging;
using SetupMeetings.Queries.Users;
using System.Collections.Generic;

namespace SetupMeetings.WebApi.Infrastracture.Queries
{
    public class UserRepositoryUppdator : 
        IEventHandler<UserCreatedEvent>,
        IEventHandler<UserEmailAddressChangedEvent>,
        IEventHandler<UserOrganizationChangedEvent>,
        IEventHandler<UserDeletedEvent>
    {
        private List<User> _dataStore;

        public UserRepositoryUppdator(List<User> dataStore)
        {

        }

        public void Handle(UserCreatedEvent @event)
        {
            throw new System.NotImplementedException();
        }

        public void Handle(UserEmailAddressChangedEvent @event)
        {
            throw new System.NotImplementedException();
        }

        public void Handle(UserOrganizationChangedEvent @event)
        {
            throw new System.NotImplementedException();
        }

        public void Handle(UserDeletedEvent @event)
        {
            throw new System.NotImplementedException();
        }
    }
}
