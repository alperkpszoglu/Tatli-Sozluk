using AutoMapper;
using MediatR;
using SozlukApp.Api.Application.Interfaces.Repositories;
using SozlukAppCommon.Models.RequestModels;

namespace SozlukApp.Api.Application.Features.Commands.Entry.Create
{
    public class CreateEntryCommandHandler: IRequestHandler<CreateEntryCommand, Guid>
    {
        private readonly IEntryRepository entryRepository;
        private readonly IMapper mapper;

        public CreateEntryCommandHandler(IEntryRepository entryRepository, IMapper mapper)
        {
            this.entryRepository = entryRepository;
            this.mapper = mapper;
        }

        public async Task<Guid> Handle(CreateEntryCommand request, CancellationToken cancellationToken)
        {
            var entry = mapper.Map<Domain.Models.Entry>(request);

            await entryRepository.AddAsync(entry);

            return entry.Id;
        }
    }
}
