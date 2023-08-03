﻿using MediatR;
using SozlukAppCommon;
using SozlukAppCommon.Events.Entry;
using SozlukAppCommon.Infrastructure;

namespace SozlukApp.Api.Application.Features.Commands.EntryComment.DeleteFav
{
    public class DeleteEntryCommentFavCommandHandler : IRequestHandler<DeleteEntryCommentFavCommand, bool>
    {

        public async Task<bool> Handle(DeleteEntryCommentFavCommand request, CancellationToken cancellationToken)
        {
            QueueFactory.SendMessageToExchange(SozlukAppConstants.FavoritesExchangeName,
                SozlukAppConstants.DefaulExchange,
                SozlukAppConstants.DeleteEntryCommentFavQueueName,
                obj: new DeleteEntryCommentFavEvent()
                {
                    UserId = request.UserId,
                    EntryCommentId = request.EntryCommentId,
                });

            return await Task.FromResult(true);
        }
    }
}
