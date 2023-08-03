using MediatR;
using SozlukAppCommon.Events.Entry;
using SozlukAppCommon.Infrastructure;
using SozlukAppCommon;

namespace SozlukApp.Api.Application.Features.Commands.EntryComment.DeleteVote
{
    public class DeleteEntryCommentVoteCommandHandler : IRequestHandler<DeleteEntryCommentVoteCommand, bool>
    {
        public async Task<bool> Handle(DeleteEntryCommentVoteCommand request, CancellationToken cancellationToken)
        {
            QueueFactory.SendMessageToExchange(SozlukAppConstants.VoteExchangeName,
                SozlukAppConstants.DefaulExchange,
                SozlukAppConstants.DeleteEntryCommentVoteQueueName,
                obj: new DeleteEntryCommentVoteEvent()
                {
                    UserId = request.UserId,
                    EntryCommentId = request.EntryCommentId,
                });

            return await Task.FromResult(true);
        }
    }
}
