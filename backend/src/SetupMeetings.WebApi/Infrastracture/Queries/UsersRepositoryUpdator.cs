using SetupMeetings.Common.Events.Users;
using SetupMeetings.Infrastructure.Messaging;
using SetupMeetings.Queries.Common;
using SetupMeetings.Queries.Users;
using System;
using SetupMeetings.WebApi.Infrastracture.DataStore;

namespace SetupMeetings.WebApi.Infrastracture.Queries
{
    public class UsersRepositoryUpdator : 
        IEventHandler<UserCreatedEvent>,
        IEventHandler<UserEmailAddressChangedEvent>,
        IEventHandler<UserOrganizationChangedEvent>,
        IEventHandler<UserDeletedEvent>
    {
        private SetupMeetingsQueryContext _context;

        public UsersRepositoryUpdator(SetupMeetingsQueryContext context)
        {
            this._context = context;
        }

        public void Handle(UserCreatedEvent @event)
        {
            var user  = _context.Users.FindById(@event.SourceId);
            if (user != null)
            {
                // error ?
            }

            user = new User()
            {
                Id = @event.SourceId,
                Name = @event.Name,
                EmailAddress = @event.EmailAddress,
                Organization = Organization.FindById(@event.OrganizationId)
            };
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void Handle(UserEmailAddressChangedEvent @event)
        {
            var user = _context.Users.FindById(@event.SourceId);
            if (user == null)
            {
                throw new ArgumentException("user does not exists in data store.", nameof(@event));
            }

            user.EmailAddress = @event.NewEmailAddress;
            _context.SaveChanges();
        }

        public void Handle(UserOrganizationChangedEvent @event)
        {
            var user = _context.Users.FindById(@event.SourceId);
            if (user == null)
            {
                throw new ArgumentException("user does not exists in data store.", nameof(@event));
            }

            user.Organization = Organization.FindById(@event.NewOrganizationId);
            _context.SaveChanges();
        }

        public void Handle(UserDeletedEvent @event)
        {
            var user = _context.Users.FindById(@event.SourceId);
            if (user == null)
            {
                throw new ArgumentException("user does not exists in data store.", nameof(@event));
            }

            _context.Users.Remove(user);
            _context.SaveChanges();
        }
    }
}
