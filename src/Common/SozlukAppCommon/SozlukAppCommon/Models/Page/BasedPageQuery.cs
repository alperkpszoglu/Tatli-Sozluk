namespace SozlukAppCommon.Models.Page
{
    public class BasedPageQuery
    {
        public int Page { get; set; }
        public int PageSize { get; set; }

        public BasedPageQuery(int page, int pageSize)
        {
            Page = page;
            PageSize = pageSize;
        }
    }
}
