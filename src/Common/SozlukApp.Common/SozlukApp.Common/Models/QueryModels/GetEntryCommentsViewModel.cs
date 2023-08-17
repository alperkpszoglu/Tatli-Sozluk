using SozlukApp.Common.Models.ResponseModels;

namespace SozlukApp.Common.Models.QueryModels
{
    public class GetEntryCommentsViewModel : BaseFooterRateFavoritedViewModel
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedByUserName { get; set; }
    }
}
