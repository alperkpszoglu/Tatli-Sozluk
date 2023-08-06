using MediatR;
using SozlukAppCommon.Models.ResponseModels;

namespace SozlukApp.Api.Application.Features.Queries.GetEntryDetail
{
    public class GetEntryDetailQuery : IRequest<GetEntryDetailViewModel>
    {
        public Guid? UserId { get; set; }
        public Guid EntryId { get; set; }

        public GetEntryDetailQuery(Guid? userId, Guid entryId)
        {
            UserId = userId;
            EntryId = entryId;
        }
    }
}
