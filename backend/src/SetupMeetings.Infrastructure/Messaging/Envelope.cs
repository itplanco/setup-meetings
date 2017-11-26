namespace SetupMeetings.Infrastructure.Messaging
{
    public class Envelope<T>
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