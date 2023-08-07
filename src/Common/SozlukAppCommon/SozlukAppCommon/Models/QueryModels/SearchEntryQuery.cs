using MediatR;

namespace SozlukAppCommon.Models.QueryModels
{
    public class SearchEntryQuery : IRequest<List<SearchBySubjectViewModel>>
    {
        public string SearchText { get; set; }

        public SearchEntryQuery(string searchText)
        {
            SearchText = searchText;
        }
    }
}
