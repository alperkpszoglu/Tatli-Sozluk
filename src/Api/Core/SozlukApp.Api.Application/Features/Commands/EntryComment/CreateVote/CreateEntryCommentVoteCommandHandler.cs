using MediatR;
using SozlukApp.Common;
using SozlukApp.Common.Events.Entry;
using SozlukApp.Common.Infrastructure;
using SozlukApp.Common.Models.RequestModels;

namespace SozlukApp.Api.Application.Features.Commands.EntryComment.CreateVote
{
    public class CreateEntryCommentVoteCommandHandler : IRequestHandler<CreateEntryCommentVoteCommand, bool>
    {
        public async Task<bool> Handle(CreateEntryCommentVoteCommand request, CancellationToken cancellationToken)
        {
            QueueFactory.SendMessageToExchange(SozlukAppConstants.VoteExchangeName,
                SozlukAppConstants.DefaulExchange,
                SozlukAppConstants.CreateEntryCommentVoteQueueName,
                obj: new CreateEntryCommentVoteEvent()
                {
                    EntryCommentId = request.EntryCommentId,
                    CreatedBy = request.CreatedBy,
                    VoteType = request.VoteType,
                });

            return await Task.FromResult(true);
        }
    }
}
