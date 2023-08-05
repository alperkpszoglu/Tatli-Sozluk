namespace SozlukAppCommon.Models.ResponseModels
{
    public class GetEntryDetailViewModel : BaseFooterRateFavoritedViewModel
    {
        public Guid Id { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public DateTime CreatedBy { get; set; }
        public string CreatedByUserName { get; set; }
    }
}
