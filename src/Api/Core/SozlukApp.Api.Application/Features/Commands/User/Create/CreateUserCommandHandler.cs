using AutoMapper;
using MediatR;
using SozlukApp.Api.Application.Interfaces.Repositories;
using SozlukAppCommon;
using SozlukAppCommon.Events.User;
using SozlukAppCommon.Infrastructure;
using SozlukAppCommon.Infrastructure.Exceptions;
using SozlukAppCommon.Models.RequestModels;

namespace SozlukApp.Api.Application.Features.Commands.User.Create
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;

        public CreateUserCommandHandler(IUserRepository userRepository, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
        }

        public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var existsUser = await userRepository.GetSingleAsync(x => x.EmailAddress == request.EmailAddress);

            if (existsUser != null)
                throw new DbValidationException("User Already Exist!!");

            var user = mapper.Map<Domain.Models.User>(request);

            var rows = await userRepository.AddAsync(user);

            // email created 

            if (rows > 0)
            {
                var _event = new UserEmailChangedEvents()
                {
                    OldEmailAddress = null,
                    NewEmailAddress = request.EmailAddress
                };

                QueueFactory.SendMessageToExchange(
                    exchangeName: SozlukAppConstants.UserExchangeName,
                    exchangeType: SozlukAppConstants.DefaulExchange,
                    queueName: SozlukAppConstants.UserEmailChangeQueueName,
                    obj: _event);
            }

            return user.Id; 
        }
    }
}
