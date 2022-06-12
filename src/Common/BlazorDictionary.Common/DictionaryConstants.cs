namespace BlazorDictionary.Common
{
    public class DictionaryConstants
    {
        public const string RabbitMQHost = "localhost";
        public const string DefaultExchangeType = "direct";

        public const string UserExchangeName = "UserExchange";
        public const string UserEmailChangedQueueName = "UserEmailChangedQueue";

        public const string FavExchangeName = "FavExchange";
        public const string CreateEntryFavQueueName = "CreateEntryFavQueue";
        public const string CreateEntryCommentFavQueueName = "CreateEntryCommentFavQueue";
        public const string CreateEntryCommentVoteQueueName = "CreateEntryCommentVoteQueue";

        public const string DeleteEntryFavQueueName = "DeleteEntryFavQueue";
        public const string DeleteEntryCommentFavQueueName = "DeleteEntryCommentFavQueue";
        public const string DeleteEntryCommentVoteQueueName = "DeleteEntryCommentVoteQueue";

        public const string DeleteEntryVoteQueueName = "DeleteEntryFavQueue";

        public const string VoteExchangeName = "VoteExchange";
        public const string CreateEntryVoteQueueName = "CreateEntryVoteQueue";
    }
}
