using MediatR;

namespace SozlukApp.Api.Application.Features.Commands.Entry.CreateFav
{
    public class CreateEntryFavCommand: IRequest<bool>
    {
        public CreateEntryFavCommand() { }

        public Guid? EntryId { get; set; }
        public Guid? UserId { get; set; }


    }
}
