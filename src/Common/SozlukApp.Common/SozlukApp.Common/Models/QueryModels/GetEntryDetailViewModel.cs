﻿namespace SozlukApp.Common.Models.ResponseModels
{
    public class GetEntryDetailViewModel : BaseFooterRateFavoritedViewModel
    {
        public Guid Id { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreatedByUserName { get; set; }
    }
}
