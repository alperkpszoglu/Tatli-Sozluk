using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SozlukApp.Api.Application.Interfaces.Repositories;
using SozlukAppCommon.Infrastructure.Extensions;
using SozlukAppCommon.Models.Page;
using SozlukAppCommon.Models.ResponseModels;
using SozlukAppCommon.ViewModels;

namespace SozlukApp.Api.Application.Features.Queries.GetMainPageEntries
{
    public class GetMainPageEntriesQueryHandler : IRequestHandler<GetMainPageEntriesQuery, PagedViewModel<GetEntryDetailViewModel>>
    {
        private readonly IEntryRepository entryRepository;

        public GetMainPageEntriesQueryHandler(IEntryRepository entryRepository)
        {
            this.entryRepository = entryRepository;
        }

        public async Task<PagedViewModel<GetEntryDetailViewModel>> Handle(GetMainPageEntriesQuery request, CancellationToken cancellationToken)
        {
            var query = entryRepository.AsQueryable();

            query = query.Include(x => x.CreatedBy)
                         .Include(x => x.EntryFavorites)
                         .Include(x => x.EntryVotes);

            var list = query.Select(i => new GetEntryDetailViewModel()
            {
                Id = i.Id,
                Content = i.Content,
                Subject = i.Subject,
                IsFavorited = request.UserId.HasValue &&
                    i.EntryFavorites.Any(q => q.CreatedById == request.UserId), // check if current user added the entry in his favorites
                FavoritedCount = i.EntryFavorites.Count,
                CreateDate = i.CreateDate,
                CreatedByUserName = i.CreatedBy.UserName,
                VoteType =
                    request.UserId.HasValue && i.EntryVotes.Any(q => q.CreatedById == request.UserId)
                    ? i.EntryVotes.FirstOrDefault(q => q.CreatedById == request.UserId).VoteType
                    : VoteType.None,
            });

            var entries = await list.GetPaged(request.CurrentPage, request.PageSize);

            return new PagedViewModel<GetEntryDetailViewModel>(entries.Results, entries.PageInfo);
        }
    }
}
