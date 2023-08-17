using MediatR;
using Microsoft.EntityFrameworkCore;
using SozlukApp.Api.Application.Interfaces.Repositories;
using SozlukApp.Common.Models.QueryModels;

namespace SozlukApp.Api.Application.Features.Queries.SearchBySubject
{
    public class SearchEntryQueryHandler : IRequestHandler<SearchEntryQuery, List<SearchBySubjectViewModel>>
    {
        private readonly IEntryRepository entryRepository;

        public SearchEntryQueryHandler(IEntryRepository entryRepository)
        {
            this.entryRepository = entryRepository;
        }

        public async Task<List<SearchBySubjectViewModel>> Handle(SearchEntryQuery request, CancellationToken cancellationToken)
        {
            var result = entryRepository.Get(e => EF.Functions.Like(e.Subject, $"%{request.SearchText}%"))
                .Select(e => new SearchBySubjectViewModel()
                {
                    Id = e.Id,
                    Subject = e.Subject
                });


            return await result.ToListAsync(cancellationToken);
        }
    }
}
