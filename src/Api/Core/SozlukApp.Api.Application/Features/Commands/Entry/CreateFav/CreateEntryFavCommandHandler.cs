using AutoMapper;
using MediatR;
using SozlukApp.Api.Application.Interfaces.Repositories;
using SozlukApp.Common;
using SozlukApp.Common.Events.Entry;
using SozlukApp.Common.Infrastructure;

namespace SozlukApp.Api.Application.Features.Commands.Entry.CreateFav
{
    public class CreateEntryFavCommandHandler : IRequestHandler<CreateEntryFavCommand, bool>
    {
        public async Task<bool> Handle(CreateEntryFavCommand request, CancellationToken cancellationToken)
        {
            QueueFactory.SendMessageToExchange(SozlukAppConstants.FavoritesExchangeName,
                SozlukAppConstants.DefaulExchange,
                SozlukAppConstants.CreateEntryFavQueueName,
                new CreateEntryFavEvent()
                {
                    EntryId = request.EntryId.Value,
                    UserId = request.UserId.Value
                });

            return await Task.FromResult(true);
        }
    }
}
