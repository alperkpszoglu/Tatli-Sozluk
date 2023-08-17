using SozlukApp.Common.ViewModels;

namespace SozlukApp.Common.Models.ResponseModels
{
    public class BaseFooterRateViewModel // with votes
    {
        public VoteType VoteType { get; set; }
    }

    public class BaseFooterFavoritedViewModel //with favorites
    {
        public bool IsFavorited { get; set; }
        public int FavoritedCount { get; set; }
    }

    public class BaseFooterRateFavoritedViewModel : BaseFooterFavoritedViewModel //with both
    {
        public VoteType VoteType { get; set; }
    }
}
