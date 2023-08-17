using AutoMapper;
using MediatR;
using SozlukApp.Api.Application.Interfaces.Repositories;
using SozlukApp.Api.Domain.Models;
using SozlukApp.Common.Models.RequestModels;

namespace SozlukApp.Api.Application.Features.Commands.EntryComment.Create
{
    public class CreateEntryCommentCommandHandler : IRequestHandler<CreateEntryCommentCommand, Guid>
    {
        private readonly IEntryCommentRepository entryCommentRepository;
        private readonly IMapper mapper;

        public async Task<Guid> Handle(CreateEntryCommentCommand request, CancellationToken cancellationToken)
        {
            var entryComment = mapper.Map<Domain.Models.EntryComment>(request);
            await entryCommentRepository.AddAsync(entryComment);

            return entryComment.Id;
        }
    }
}
