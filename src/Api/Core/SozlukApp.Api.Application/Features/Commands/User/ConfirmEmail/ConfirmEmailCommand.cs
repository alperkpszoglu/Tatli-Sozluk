using MediatR;
using SozlukApp.Api.Application.Interfaces.Repositories;
using SozlukAppCommon.Infrastructure.Exceptions;

namespace SozlukApp.Api.Application.Features.Commands.User.ConfirmEmail
{
    public class ConfirmEmailCommand : IRequest<bool>
    {
        public Guid ConfirmationId { get; set; }
    }


    public class ConfirmEmailCommandHandler : IRequestHandler<ConfirmEmailCommand, bool>
    {
        private readonly IEmailConfirmationRepository emailConfirmationRepository;
        private readonly IUserRepository userRepository;

        public ConfirmEmailCommandHandler(IEmailConfirmationRepository emailConfirmationRepository,
            IUserRepository userRepository)
        {
            this.emailConfirmationRepository = emailConfirmationRepository;
            this.userRepository = userRepository;
        }


        public async Task<bool> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
        {
            var confirmation = await emailConfirmationRepository.GetByIdAsync(request.ConfirmationId);

            if (confirmation is null)
                throw new DbValidationException("Confirmation not found..");

            var user = await userRepository.GetSingleAsync(x => x.EmailAddress == confirmation.NewEmailAdress);

            if (user is null)
                throw new DbValidationException("There is no user with this email..");

            if (user.EmailConfirmed)
                throw new DbValidationException("Email address already confirmed for this user..");

            user.EmailConfirmed = true;
            await userRepository.UpdateAsync(user);

            return await Task.FromResult(true);
        }
    }
}
