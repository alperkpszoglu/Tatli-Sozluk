using AutoMapper;
using MediatR;
using SozlukApp.Api.Application.Interfaces.Repositories;
using SozlukApp.Common;
using SozlukApp.Common.Events.User;
using SozlukApp.Common.Infrastructure;
using SozlukApp.Common.Infrastructure.Exceptions;
using SozlukApp.Common.Models.RequestModels;

namespace SozlukApp.Api.Application.Features.Commands.User.Update
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Guid>
    {
        private readonly IMapper mapper;
        private readonly IUserRepository userRepository;

        public UpdateUserCommandHandler(IMapper mapper, IUserRepository userRepository)
        {
            this.mapper = mapper;
            this.userRepository = userRepository;
        }

        public async Task<Guid> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetByIdAsync(request.Id);

            if (user == null)
                throw new DbValidationException("User not found!!");

            var dbEmailAddress = user.EmailAddress;
            var isEmailChanged = string.CompareOrdinal(dbEmailAddress, request.EmailAddress) != 0;

            mapper.Map(request, user); // we don't need to create a new instance with .Map function

            var rows = await userRepository.UpdateAsync(user);

            // email changed
            
            // To Do Email change will validate
            if (rows > 0 && isEmailChanged)
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

                user.EmailConfirmed = false;
                await userRepository.UpdateAsync(user);
            }


            return user.Id;
        }
    }
}
