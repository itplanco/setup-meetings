using System;

namespace SetupMeetings.Infrastructure.EventSourcing
{

    public interface IEventSourcedRepository<T> where T : IEventSourced
    {
        T Find(Guid id);
        T Get(Guid userId);
        void Save(T eventSourced, Guid correlationId);
    }
}
