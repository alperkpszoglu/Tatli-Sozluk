using MediatR;
using SozlukApp.Api.Application.Interfaces.Repositories;
using SozlukApp.Common.Events.User;
using SozlukApp.Common.Infrastructure;
using SozlukApp.Common.Infrastructure.Exceptions;

namespace SozlukApp.Api.Application.Features.Commands.User.ChangePassword
{
    public class ChangeUserPasswordCommandHandler : IRequestHandler<ChangeUserPasswordCommand, bool>
    {
        private readonly IUserRepository userRepository;

        public ChangeUserPasswordCommandHandler(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<bool> Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
        {
            if(request.UserId.HasValue)
            {
                throw new ArgumentNullException(nameof(request.UserId)); 
            }

            var user = await userRepository.GetByIdAsync(request.UserId.Value);

            if (user == null)
                throw new DbValidationException("User not Found!"); 

            var hashPassword = PasswordEncryptor.Encrypt(request.OldPassword);
            if(user.Password != hashPassword)
                throw new DbValidationException("Old Password and password don't match!");
             
            user.Password = PasswordEncryptor.Encrypt(request.NewPassword);

            await userRepository.UpdateAsync(user);

            return true;

        }
    }
}
