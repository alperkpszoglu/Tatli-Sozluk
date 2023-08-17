using MediatR;
using Microsoft.EntityFrameworkCore;
using SozlukApp.Api.Application.Interfaces.Repositories;
using SozlukApp.Common.Models.Page;
using SozlukApp.Common.Models.QueryModels;
using SozlukApp.Common.Models.ResponseModels;
using SozlukApp.Common.ViewModels;
using SozlukApp.Common.Infrastructure.Extensions;

namespace SozlukApp.Api.Application.Features.Queries.GetEntryComments
{
    public class GetEntryCommentQueryHandler : IRequestHandler<GetEntryCommentsQuery, PagedViewModel<GetEntryCommentsViewModel>>
    {
        private readonly IEntryCommentRepository entryCommentRepository;

        public async Task<PagedViewModel<GetEntryCommentsViewModel>> Handle(GetEntryCommentsQuery request, CancellationToken cancellationToken)
        {
            var query = entryCommentRepository.AsQueryable();

            query = query.Include(x => x.CreatedBy)
                         .Include(x => x.EntryCommentFavorites)
                         .Include(x => x.EntryCommentVotes)
                         .Where(x => x.EntryId == request.EntryId);

            var list = query.Select(i => new GetEntryCommentsViewModel()
            {
                Id = i.Id,
                Content = i.Content,
                IsFavorited = request.UserId.HasValue &&
                    i.EntryCommentFavorites.Any(q => q.CreatedById == request.UserId),
                FavoritedCount = i.EntryCommentFavorites.Count,
                CreatedDate = i.CreateDate,
                CreatedByUserName = i.CreatedBy.UserName,
                VoteType =
                    request.UserId.HasValue && i.EntryCommentVotes.Any(q => q.CreatedById == request.UserId)
                    ? i.EntryCommentVotes.FirstOrDefault(q => q.CreatedById == request.UserId).VoteType
                    : VoteType.None,
            });

            var entryComments = await list.GetPaged(request.CurrentPage, request.PageSize);

            return entryComments;
        }
    }
}
