using MediatR;
using SozlukAppCommon.Models.Queries;

namespace SozlukApp.Api.Application.Features.Queries.GetEntries
{
    public class GetEntriesQuery: IRequest<List<GetEntriesViewModel>>
    {
        public bool TodaysEntries { get; set; }
        public int Count { get; set; } = 100; // how many entries will be there in menu
    }
}
