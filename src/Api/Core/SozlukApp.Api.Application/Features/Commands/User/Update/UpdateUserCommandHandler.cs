using AutoMapper;
using MediatR;
using SozlukApp.Api.Application.Interfaces.Repositories;
using SozlukAppCommon.Events.User;
using SozlukAppCommon.Infrastructure;
using SozlukAppCommon;
using SozlukAppCommon.Infrastructure.Exceptions;
using SozlukAppCommon.Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
