using MediatR;
using SozlukAppCommon;
using SozlukAppCommon.Events.Entry;
using SozlukAppCommon.Infrastructure;

namespace SozlukApp.Api.Application.Features.Commands.Entry.DeleteVote
{
    public class DeleteEntryVoteCommandHandler : IRequestHandler<DeleteEntryVoteCommand, bool>
    {
        public async Task<bool> Handle(DeleteEntryVoteCommand request, CancellationToken cancellationToken)
        {
            QueueFactory.SendMessageToExchange(SozlukAppConstants.VoteExchangeName,
                SozlukAppConstants.DefaulExchange,
                SozlukAppConstants.DeleteEntryVoteQueueName,
                new DeleteEntryVoteEvent()
                {
                    EntryId = request.EntryId,
                    UserId = request.UserId,
                });

            return await Task.FromResult(true);
        }
    }
}
