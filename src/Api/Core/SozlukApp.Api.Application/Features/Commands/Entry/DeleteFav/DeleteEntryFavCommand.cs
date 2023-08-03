using MediatR;

namespace SozlukApp.Api.Application.Features.Commands.Entry.DeleteFav
{
    public class DeleteEntryFavCommand: IRequest<bool>
    {
        public Guid EntryId { get; set; }
        public Guid UserId { get; set; }

        public DeleteEntryFavCommand(Guid userId, Guid entryId)
        {
            UserId = userId;
            EntryId = entryId;
        }
    }
}
