using MediatR;
using SozlukAppCommon.Models.Page;
using SozlukAppCommon.Models.ResponseModels;

namespace SozlukApp.Api.Application.Features.Queries.GetMainPageEntries
{
    public class GetMainPageEntriesQuery : BasedPageQuery, IRequest<PagedViewModel<GetEntryDetailViewModel>>
    {
        public Guid? UserId { get; set; }

        public GetMainPageEntriesQuery(int currentPage, int pageSize, Guid? userId) : base(currentPage, pageSize)
        {
            UserId = userId;
        }
    }
}
