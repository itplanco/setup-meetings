namespace SetupMeetings.Queries
{
    public class User
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public Organization Organization { get; set; }
    }
}