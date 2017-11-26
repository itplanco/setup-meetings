using System;

namespace SetupMeetings.Infrastructure.Messaging
{
    public interface ICommand
    {
        Guid Id { get; }
    }
}