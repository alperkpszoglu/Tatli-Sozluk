using SozlukApp.Common.Models.Page;
using SozlukApp.Common.Models.Queries;
using SozlukApp.Common.Models.QueryModels;
using SozlukApp.Common.Models.RequestModels;
using SozlukApp.Common.Models.ResponseModels;

namespace SozlukApp.WebApp.Infrastructure.Services.Interfaces
{
    public interface IEntryService
    {
        Task<Guid> CreateEntry(CreateEntryCommand command);
        Task<Guid> CreateEntryComment(CreateEntryCommentCommand command);
        Task<GetEntryDetailViewModel> GetEntrieDetail(Guid entryId);
        Task<List<GetEntriesViewModel>> GetEntries();
        Task<PagedViewModel<GetEntryCommentsViewModel>> GetEntryComments(Guid entryId, int page, int pageSize);
        Task<PagedViewModel<GetEntryDetailViewModel>> GetMainPageEntries(int currentPage, int pageSize);
        Task<PagedViewModel<GetUserEntriesViewModel>> GetProfilePageEntries(int page, int pageSize, string userName = null);
        Task<List<SearchBySubjectViewModel>> Search(string searchText);
    }
}