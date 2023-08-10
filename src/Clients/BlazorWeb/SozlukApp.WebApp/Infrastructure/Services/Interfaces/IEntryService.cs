using SozlukAppCommon.Models.Page;
using SozlukAppCommon.Models.Queries;
using SozlukAppCommon.Models.QueryModels;
using SozlukAppCommon.Models.RequestModels;
using SozlukAppCommon.Models.ResponseModels;

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