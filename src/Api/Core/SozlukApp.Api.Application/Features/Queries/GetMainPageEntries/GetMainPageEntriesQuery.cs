using MediatR;
using SozlukApp.Common.Models.Page;
using SozlukApp.Common.Models.ResponseModels;

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
