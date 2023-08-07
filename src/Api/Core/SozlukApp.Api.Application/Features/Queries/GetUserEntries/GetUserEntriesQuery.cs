using MediatR;
using SozlukAppCommon.Models.Page;
using SozlukAppCommon.Models.QueryModels;

namespace SozlukApp.Api.Application.Features.Queries.GetUserEntries
{
    public class GetUserEntriesQuery : BasedPageQuery, IRequest<PagedViewModel<GetUserEntriesViewModel>>
    {
        public Guid? UserId { get; set; }
        public string UserName { get; set; }

        public GetUserEntriesQuery(Guid? userId, string userName = null, int currentPage = 1, int pageSize = 10) : base(currentPage, pageSize)
        {
            UserId = userId;
            UserName = userName;
        }
    }
}
