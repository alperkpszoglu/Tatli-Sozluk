namespace SozlukApp.Common
{
    public class SozlukAppConstants
    {
        // rabbitMQ
        public const string RabbitHost = "localhost";
        public const string DefaulExchange = "direct";

        // user
        public const string UserExchangeName = "UserExchange";
        public const string UserEmailChangeQueueName = "UserEmailChangeQueue";

        
        // fav
        public const string FavoritesExchangeName = "FavExchangeName";
        public const string CreateEntryFavQueueName = "CreateEntryFavQueue";
        public const string DeleteEntryFavQueueName = "DeleteEntryFavQueue";
        
        public const string CreateEntryCommentFavQueueName = "CreateEntryCommentFavQueue";
        public const string DeleteEntryCommentFavQueueName = "DeleteEntryCommentFavQueue";


        // votes
        public const string VoteExchangeName = "VoteExchangeName";
        public const string CreateEntryVoteQueueName = "CreateEntryVoteQueue";
        public const string DeleteEntryVoteQueueName = "DeleteEntryVoteQueue";

        public const string CreateEntryCommentVoteQueueName = "CreateEntryCommentVoteQueue";
        public const string DeleteEntryCommentVoteQueueName = "DeleteEntryCommentVoteQueue";



    }
}
