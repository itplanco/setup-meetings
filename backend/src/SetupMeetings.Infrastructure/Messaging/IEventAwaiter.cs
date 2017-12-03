using System;
using System.Threading.Tasks;

namespace SetupMeetings.Infrastructure.Messaging
{
    public interface IEventAwaiter
    {
        Task<T> WaitForMessage<T>(Guid correlationId, TimeSpan timeout) where T : IEvent;
    }
}
