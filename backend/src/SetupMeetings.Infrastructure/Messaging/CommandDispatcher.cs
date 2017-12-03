using SetupMeetings.Infrastructure.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SetupMeetings.Infrastructure.Messaging
{
    public class CommandDispatcher
    {
        private Dictionary<Type, ICommandHandler> handlers = new Dictionary<Type, ICommandHandler>();

        public CommandDispatcher()
        {
        }

        public void Register(ICommandHandler commandHandler)
        {
            var genericHandler = typeof(ICommandHandler<>);
            var supportedCommandTypes = commandHandler.GetType()
                .GetInterfaces()
                .Where(iface => iface.IsGenericType && iface.GetGenericTypeDefinition() == genericHandler)
                .Select(iface => iface.GetGenericArguments()[0])
                .ToList();

            if (handlers.Keys.Any(registeredType => supportedCommandTypes.Contains(registeredType)))
                throw new ArgumentException("The command handled by the received handler already has a registered handler.");

            foreach (var commandType in supportedCommandTypes)
            {
                this.handlers.Add(commandType, commandHandler);
            }
        }

        public bool ProcessMessage(ICommand payload)
        {
            var commandType = payload.GetType();
            ICommandHandler handler = null;

            if (handlers.TryGetValue(commandType, out handler))
            {
                ((dynamic)handler).Handle((dynamic)payload);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
