using System;
using System.Threading.Tasks;
using SetupMeetings.Infrastructure.Messaging;

namespace SetupMeetings.WebApi.Infrastracture.Messaging
{
    public class EventAwaiter : IEventAwaiter
    {
        class OneTimeEventHandler<T> : IEnvelopedEventHandler<T>
            where T : IEvent
        {
            private Guid _waitId;
            private TaskCompletionSource<T> _taskCompletionSource;

            public OneTimeEventHandler(Guid waitId, TaskCompletionSource<T> taskCompletionSource)
            {
                _waitId = waitId;
                _taskCompletionSource = taskCompletionSource;
            }

            public void Handle(Envelope<T> @event)
            {
                if (@event.CorrelationId == _waitId.ToString())
                {
                    _taskCompletionSource.SetResult(@event.Body);
                }
            }
        }
        private EventDispatcher _eventDispatcher;

        public EventAwaiter(EventDispatcher dispatcher)
        {
            _eventDispatcher = dispatcher;
        }

        public Task<T> WaitForMessage<T>(Guid correlationId, TimeSpan timeout)
            where T : IEvent
        {
            var taskCompletionSource = new TaskCompletionSource<T>();
            _eventDispatcher.Register(new OneTimeEventHandler<T>(correlationId, taskCompletionSource));
            return Task.Run(() =>
            {
                var task = taskCompletionSource.Task;
                task.Wait(timeout);
                return task.IsCompleted ? task.Result : default(T);
            });
        }
    }
}
