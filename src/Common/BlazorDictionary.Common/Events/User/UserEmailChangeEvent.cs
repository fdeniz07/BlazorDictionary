namespace BlazorDictionary.Common.Events.User
{
    public class UserEmailChangeEvent
    {
        public string OldEmailAddress { get; set; }

        public string NewEmailAddress { get; set; }

    }
}
