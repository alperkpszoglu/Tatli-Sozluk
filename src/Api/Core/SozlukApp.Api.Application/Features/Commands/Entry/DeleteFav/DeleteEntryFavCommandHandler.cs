using MediatR;
using SozlukApp.Common;
using SozlukApp.Common.Events.Entry;
using SozlukApp.Common.Infrastructure;

namespace SozlukApp.Api.Application.Features.Commands.Entry.DeleteFav
{
    public class DeleteEntryFavCommandHandler : IRequestHandler<DeleteEntryFavCommand, bool>
    {
        public async Task<bool> Handle(DeleteEntryFavCommand request, CancellationToken cancellationToken)
        {
            QueueFactory.SendMessageToExchange(SozlukAppConstants.FavoritesExchangeName,
                SozlukAppConstants.DefaulExchange,
                SozlukAppConstants.DeleteEntryFavQueueName,
                new DeleteEntryFavEvent()
                {
                    EntryId = request.EntryId,
                    UserId = request.UserId,
                });

            return await Task.FromResult(true);
        }
    }
}
