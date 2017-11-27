using System;

namespace SetupMeetings.Infrastructure.Messaging
{
    public abstract class Envelope
    {
        public static Envelope<T> Create<T>(T body)
        {
            return new Envelope<T>(body);
        }
    }

    public class Envelope<T> : Envelope
    {
        public Envelope(T body)
        {
            Body = body;
        }

        public T Body { get; }

        public Envelope<T> Create(T body)
        {
            return new Envelope<T>(body);
        }
    }
}