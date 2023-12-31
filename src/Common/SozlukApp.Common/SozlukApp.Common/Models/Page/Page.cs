﻿namespace SozlukApp.Common.Models.Page
{
    public class Page
    {
        public Page():this(0){}

        public Page(int totalRowCount): this(1,10,totalRowCount)
        {
                
        }

        public Page(int pageSize, int totalRowCount): this(1, pageSize, totalRowCount)
        {}

        public Page(int currentPage, int pageSize, int totalRowCount)
        {
            if (currentPage < 1)
                throw new ArgumentException("Current page cannot be less than 1");
            if(pageSize < 1)
                throw new ArgumentException("Page size cannot be less than 1");

            this.TotalRowCount = totalRowCount;
            this.PageSize = pageSize;
            this.CurrentPage = currentPage;
        }

        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalRowCount { get; set; }
        public int TotalPagesCount => (int)Math.Ceiling((double)TotalRowCount / PageSize);
        public int Skip => (CurrentPage-1) * PageSize;
    }
}
