using SozlukAppCommon.ViewModels;

namespace SozlukAppCommon.Events.Entry
{
    public class CreateEntryCommentVoteEvent
    {
        public Guid EntryCommentId { get; set; }
        public VoteType VoteType { get; set; }
        public Guid CreatedBy{ get; set; }


    }
}
