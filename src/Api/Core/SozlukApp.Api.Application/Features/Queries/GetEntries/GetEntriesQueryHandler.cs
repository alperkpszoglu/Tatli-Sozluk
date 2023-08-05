using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SozlukApp.Api.Application.Interfaces.Repositories;
using SozlukAppCommon.Models.Queries;

namespace SozlukApp.Api.Application.Features.Queries.GetEntries
{
    public class GetEntriesQueryHandler : IRequestHandler<GetEntriesQuery, List<GetEntriesViewModel>>
    {
        private readonly IEntryRepository entryRepository;
        private readonly IMapper mapper;

        public GetEntriesQueryHandler(IEntryRepository entryRepository, IMapper mapper)
        {
            this.entryRepository = entryRepository;
            this.mapper = mapper;
        }

        public async Task<List<GetEntriesViewModel>> Handle(GetEntriesQuery request, CancellationToken cancellationToken)
        {
            var query = entryRepository.AsQueryable();

            if(request.TodaysEntries == true)
            {
                query = query.Where(i => i.CreateDate >= DateTime.Now.Date)
                    .Where(i => i.CreateDate <= DateTime.Now.AddDays(1).Date);
            }

            query = query.Include(x => x.EntryComments)
                .OrderByDescending(x => x.CreatedBy)
                .Take(request.Count);

            return await query.ProjectTo<GetEntriesViewModel>(mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }
    }
}
