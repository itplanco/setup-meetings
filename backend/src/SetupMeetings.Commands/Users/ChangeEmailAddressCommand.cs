namespace SetupMeetings.Commands.Users
{
    public class ChangeEmailAddressCommand : UserCommand
    {
        public string NewEmailAddress { get; set; }
    }
}
