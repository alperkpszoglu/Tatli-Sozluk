using AutoMapper;
using MediatR;
using SozlukApp.Api.Application.Interfaces.Repositories;
using SozlukApp.Api.Domain.Models;
using SozlukAppCommon.Models.QueryModels;

namespace SozlukApp.Api.Application.Features.Queries.GetUserDetail
{
    public class GetUserDetailQueryHandler : IRequestHandler<GetUserDetailQuery, UserDetailViewModel>
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;

        public GetUserDetailQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
        }

        public async Task<UserDetailViewModel> Handle(GetUserDetailQuery request, CancellationToken cancellationToken)
        {
            User user = null;

            if (request.UserId != Guid.Empty)
                user = await userRepository.GetByIdAsync(request.UserId);
            else if (!string.IsNullOrEmpty(request.UserName))
                user = await userRepository.GetSingleAsync(x => x.UserName == request.UserName);
            else
                throw new NullReferenceException("Both UserId and UserName are null.");


            return mapper.Map<UserDetailViewModel>(user);
        }
    }
}
