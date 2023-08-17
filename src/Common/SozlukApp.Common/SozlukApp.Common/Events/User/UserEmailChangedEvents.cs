namespace SozlukApp.Common.Events.User
{
    public class UserEmailChangedEvents
    {
        // we dont take id bc email address is unique
        public string NewEmailAddress { get; set; }
        public string OldEmailAddress { get; set; }

    }
}
