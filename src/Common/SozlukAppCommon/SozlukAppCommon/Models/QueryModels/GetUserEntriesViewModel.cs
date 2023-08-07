using SozlukAppCommon.Models.ResponseModels;

namespace SozlukAppCommon.Models.QueryModels
{
    public class GetUserEntriesViewModel : BaseFooterFavoritedViewModel
    {
        public Guid Id { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedByUserName { get; set; }

    }
}
