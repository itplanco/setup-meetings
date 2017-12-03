using System.Collections.Generic;
using SetupMeetings.Infrastructure.Messaging;
using System.Linq;

namespace SetupMeetings.WebApi.Infrastracture.Messaging
{
    public class EventBus : IEventBus
    {
        private EventDispatcher _dispatcher;

        public EventBus(EventDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        public void Publish(Envelope<IEvent> @event)
        {
            _dispatcher.DispatchMessage(@event.Body, @event.MessageId, @event.CorrelationId, "");
        }

        public void Publish(IEnumerable<Envelope<IEvent>> events)
        {
            events.ToList().ForEach(e => Publish(e));
        }
    }
}
