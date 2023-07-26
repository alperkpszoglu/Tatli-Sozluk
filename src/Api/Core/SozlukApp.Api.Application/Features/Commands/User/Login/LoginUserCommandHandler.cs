using AutoMapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SozlukApp.Api.Application.Interfaces.Repositories;
using SozlukAppCommon.Infrastructure;
using SozlukAppCommon.Infrastructure.Exceptions;
using SozlukAppCommon.Models.Queries;
using SozlukAppCommon.Models.RequestModels;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SozlukApp.Api.Application.Features.Commands.User
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginUserViewModel>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;  

        public LoginUserCommandHandler(IUserRepository userRepository, IMapper mapper, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _configuration = configuration;
        }

        async Task<LoginUserViewModel> IRequestHandler<LoginUserCommand, LoginUserViewModel>.Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetSingleAsync(x => x.EmailAddress == request.EmailAdress);

            if (user == null)
                throw new DbValidationException("User not found!!");
            
            var password = PasswordEncryptor.Encrypt(request.Password);

            if (user.Password != password)
                throw new DbValidationException("Password is wrong!!");

            if (!user.EmailConfirmed)
                throw new DbValidationException("You have to confirm your Email address");
            
            var result = _mapper.Map<LoginUserViewModel>(user);
            var claims = new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.EmailAddress),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.GivenName, user.FirstName),
                new Claim(ClaimTypes.Surname, user.LastName),
            };

            result.Token = GenerateToken(claims);

            return result;
        }

        private string GenerateToken(Claim[] claims)
        {   
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AuthConfig:Secret"]));
            var creds = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);
            var expiry = DateTime.Now.AddDays(10);

            var token = new JwtSecurityToken(claims: claims,
                                            signingCredentials: creds,
                                            expires:  expiry,
                                            notBefore: DateTime.Now);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
