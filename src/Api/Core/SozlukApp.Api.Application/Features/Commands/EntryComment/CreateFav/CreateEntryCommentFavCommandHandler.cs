﻿using MediatR;
using SozlukApp.Common;
using SozlukApp.Common.Events.EntryComment;
using SozlukApp.Common.Infrastructure;

namespace SozlukApp.Api.Application.Features.Commands.EntryComment.CreateFav
{
    public class CreateEntryCommentFavCommandHandler : IRequestHandler<CreateEntryCommentFavCommand, bool>
    {


        public async Task<bool> Handle(CreateEntryCommentFavCommand request, CancellationToken cancellationToken)
        {
            QueueFactory.SendMessageToExchange(exchangeName: SozlukAppConstants.UserExchangeName,
                exchangeType: SozlukAppConstants.DefaulExchange,
                queueName: SozlukAppConstants.CreateEntryCommentFavQueueName,
                obj: new CreateEntryCommentFavEvent() { CreatedBy = request.UserId, EntryCommentId = request.EntryCommentId });

            return await Task.FromResult(true);
        }
    }
}