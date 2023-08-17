using MediatR;

namespace SozlukApp.Common.Models.RequestModels
{
    public class CreateEntryCommand: IRequest<Guid>
    {
        public Guid EntryId { get; set; }
        public Guid? CreatedById { get; set; }
        public string Content { get; set; }

        public CreateEntryCommand(Guid entryId, Guid? createdById, string content)
        {
            EntryId = entryId;
            CreatedById = createdById;
            Content = content;
        }

        public CreateEntryCommand()
        {
                
        }

    }
}
