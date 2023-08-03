using MediatR;
using SozlukApp.Api.Application.Interfaces.Repositories;
using SozlukAppCommon;
using SozlukAppCommon.Events.Entry;
using SozlukAppCommon.Infrastructure;
using SozlukAppCommon.Models.RequestModels;

namespace SozlukApp.Api.Application.Features.Commands.Entry.CreateVote
{
    public class CreateEntryVoteCommandHandler : IRequestHandler<CreateEntryVoteCommand, bool>
    {
        public async Task<bool> Handle(CreateEntryVoteCommand request, CancellationToken cancellationToken)
        {
            QueueFactory.SendMessageToExchange(SozlukAppConstants.VoteExchangeName,
                SozlukAppConstants.DefaulExchange,
                SozlukAppConstants.CreateEntryVoteQueueName,
                new CreateEntryVoteEvent()
                {
                    EntryId = request.EntryId,
                    CreatedBy = request.CreatedBy,
                    VoteType = request.VoteType
                });

            return await Task.FromResult(true);
        }
    }
}
