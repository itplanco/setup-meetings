using SetupMeetings.Commands.Meetings;
using SetupMeetings.Commands.Users;
using SetupMeetings.Infrastructure.EventSourcing;
using SetupMeetings.Infrastructure.Messaging;
using SetupMeetings.WebApi.Infrastracture.EventSourcing;
using System.Collections.Generic;
using System.Linq;

namespace SetupMeetings.WebApi.Infrastracture.Messaging
{
    public class CommandBus : ICommandBus
    {
        private CommandDispatcher _commandDispatcher;

        public CommandBus(IEventSourcedRepository<UserAggregate> usersRepository, IEventSourcedRepository<MeetingAggregate> meetingsRepository)
        {
            _commandDispatcher = new CommandDispatcher();
            _commandDispatcher.Register(new UserCommandHandler(usersRepository));
            _commandDispatcher.Register(new MeetingCommandHandler(meetingsRepository));
        }

        public void Send(Envelope<ICommand> command)
        {
            _commandDispatcher.ProcessMessage(command.Body);
        }

        public void Send(IEnumerable<Envelope<ICommand>> commands)
        {
            commands.ToList().ForEach(command => Send(command));
        }
    }
}
