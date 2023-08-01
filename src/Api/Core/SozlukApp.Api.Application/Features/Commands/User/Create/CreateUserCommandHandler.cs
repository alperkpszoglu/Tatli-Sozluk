using AutoMapper;
using MediatR;
using SozlukApp.Api.Application.Interfaces.Repositories;
using SozlukAppCommon.Infrastructure.Exceptions;
using SozlukAppCommon.Models.RequestModels;

namespace SozlukApp.Api.Application.Features.Commands.User.Create
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, bool>
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;

        public CreateUserCommandHandler(IUserRepository userRepository, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
        }

        public async Task<bool> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var existsUser = await userRepository.GetSingleAsync(x => x.EmailAddress ==  request.EmailAddress);

            if(existsUser != null)
                throw new DbValidationException("User Already Exist!!");
            
            var user = mapper.Map<Domain.Models.User>(request);

            var rows = await userRepository.AddAsync(user);

            
        }
    }
}
