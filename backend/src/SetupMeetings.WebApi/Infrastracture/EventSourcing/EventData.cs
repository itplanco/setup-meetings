namespace SetupMeetings.WebApi.Infrastracture.EventSourcing
{
    public class EventData
    {
        public string SourceId { get; set; }
        public int Version { get; set; }
        public string SourceType { get; set; }
        public string Payload { get; set; }
        public string CorrelationId { get; set; }
    }
}
