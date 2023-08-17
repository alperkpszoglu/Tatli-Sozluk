using MediatR;
using Microsoft.EntityFrameworkCore;
using SozlukApp.Api.Application.Interfaces.Repositories;
using SozlukApp.Common.Models.Page;
using SozlukApp.Common.Models.ResponseModels;
using SozlukApp.Common.ViewModels;

namespace SozlukApp.Api.Application.Features.Queries.GetEntryDetail
{
    public class GetEntryDetailQueryHandler : IRequestHandler<GetEntryDetailQuery, GetEntryDetailViewModel>
    {
        private readonly IEntryRepository entryRepository;

        public GetEntryDetailQueryHandler(IEntryRepository entryRepository)
        {
            this.entryRepository = entryRepository;
        }

        public async Task<GetEntryDetailViewModel> Handle(GetEntryDetailQuery request, CancellationToken cancellationToken)
        {
            var query = entryRepository.AsQueryable();

            query = query.Include(x => x.CreatedBy)
                         .Include(x => x.EntryFavorites)
                         .Include(x => x.EntryVotes)
                         .Include(x => x.Id == request.EntryId);

            var list = query.Select(i => new GetEntryDetailViewModel()
            {
                Id = i.Id,
                Content = i.Content,
                Subject = i.Subject,
                IsFavorited = request.UserId.HasValue &&
                    i.EntryFavorites.Any(q => q.CreatedById == request.UserId),
                FavoritedCount = i.EntryFavorites.Count,
                CreateDate = i.CreateDate,
                CreatedByUserName = i.CreatedBy.UserName,
                VoteType =
                    request.UserId.HasValue && i.EntryVotes.Any(q => q.CreatedById == request.UserId)
                    ? i.EntryVotes.FirstOrDefault(q => q.CreatedById == request.UserId).VoteType
                    : VoteType.None,
            });

            return await list.FirstOrDefaultAsync(cancellationToken);
        }
    }
}
