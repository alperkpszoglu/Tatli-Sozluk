﻿using MediatR;

namespace SozlukApp.Common.Models.RequestModels
{
    public class CreateEntryCommentCommand : IRequest<Guid>
    {
        public Guid? EntryId { get; set; }
        public string Content { get; set; }
        public Guid? CreatedById { get; set; }

        public CreateEntryCommentCommand(Guid entryId, string content, Guid createdById)
        {
            EntryId = entryId;
            Content = content;
            CreatedById = createdById;
        }

        public CreateEntryCommentCommand() { }

    }
}
