using Microsoft.EntityFrameworkCore;
using SozlukApp.Common.Models.Page;

namespace SozlukApp.Common.Infrastructure.Extensions
{
    public static class PagingExtensions
    {
        public static async Task<PagedViewModel<T>> GetPaged<T>(this IQueryable<T> query,
            int currentPage,
            int pageSize) where T : class
        {
            var count = await query.CountAsync(); // total entry

            Page page = new(currentPage, pageSize, count);

            var data = await query.Skip(page.Skip).Take(page.PageSize).AsNoTracking().ToListAsync();

            var result = new PagedViewModel<T>(data, page);

            return result;
        }
    }
}
