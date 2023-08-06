using MediatR;
using SozlukAppCommon.Models.Page;
using SozlukAppCommon.Models.QueryModels;

namespace SozlukApp.Api.Application.Features.Queries.GetEntryComments
{
    public class GetEntryCommentsQuery : BasedPageQuery, IRequest<PagedViewModel<GetEntryCommentsViewModel>>
    {
        public Guid EntryId { get; set; }
        public Guid? UserId { get; set; }

        public GetEntryCommentsQuery(Guid entryId, Guid? userId, int currentPage, int pageSize)
            : base(currentPage, pageSize)
        {
            this.EntryId = entryId;
            this.UserId = userId;
        }
    }
}
