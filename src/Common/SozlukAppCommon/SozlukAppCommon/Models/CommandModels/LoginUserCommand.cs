using MediatR;
using SozlukAppCommon.Models.Queries;

namespace SozlukAppCommon.Models.RequestModels
{
    public class LoginUserCommand: IRequest<LoginUserViewModel>
    {
        public string EmailAdress { get; set; }
        public string Password { get; set; }

        public LoginUserCommand(string emailAdress, string password)
        {
            EmailAdress = emailAdress;
            Password = password;
        }

        public LoginUserCommand()
        {
                
        }
    }
}
