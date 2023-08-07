using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SozlukApp.Api.Application.Interfaces.Repositories;
using SozlukAppCommon.Infrastructure.Extensions;
using SozlukAppCommon.Models.Page;
using SozlukAppCommon.Models.QueryModels;

namespace SozlukApp.Api.Application.Features.Queries.GetUserEntries
{
    public class GetUserEntriesQueryHandler : IRequestHandler<GetUserEntriesQuery, PagedViewModel<GetUserEntriesViewModel>>
    {
        private readonly IEntryRepository entryRepository;
        private readonly IMapper mapper;

        public GetUserEntriesQueryHandler(IEntryRepository entryRepository, IMapper mapper)
        {
            this.entryRepository = entryRepository;
            this.mapper = mapper;
        }

        public async Task<PagedViewModel<GetUserEntriesViewModel>> Handle(GetUserEntriesQuery request, CancellationToken cancellationToken)
        {
            var query = entryRepository.AsQueryable();

            if (request.UserId != null && request.UserId.HasValue && request.UserId != Guid.Empty)
                query = query.Where(e => e.CreatedById == request.UserId);
            else if (!string.IsNullOrEmpty(request.UserName))
                query = query.Where(e => request.UserName == e.CreatedBy.UserName);
            else
                throw new NullReferenceException("Both UserId and UserName are null.");

            query = query.Include(i => i.EntryFavorites)
                         .Include(i => i.CreatedBy);

            var list = query.Select(e => new GetUserEntriesViewModel()
            {
                Id = e.Id,
                Subject = e.Subject,
                Content = e.Content,
                IsFavorited = false,
                FavoritedCount = e.EntryFavorites.Count,
                CreatedByUserName = e.CreatedBy.UserName,
                CreatedDate = e.CreateDate
            });

            var entries = await list.GetPaged(request.CurrentPage, request.PageSize);

            return entries;
        }
    }
}
