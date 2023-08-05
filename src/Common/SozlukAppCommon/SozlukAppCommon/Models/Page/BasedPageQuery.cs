namespace SozlukAppCommon.Models.Page
{
    public class BasedPageQuery
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }

        public BasedPageQuery(int currentPage, int pageSize)
        {
            CurrentPage = currentPage;
            PageSize = pageSize;
        }
    }
}
