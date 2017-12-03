using System;

namespace SetupMeetings.Infrastructure.EventSourcing
{

    public interface IEventSourcedRepository<T> where T : IEventSourced
    {
        T Find(Guid id);
        void Save(T eventSourced, string correlationId);
    }
}
