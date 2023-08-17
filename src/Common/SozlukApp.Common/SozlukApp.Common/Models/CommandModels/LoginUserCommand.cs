using MediatR;
using SozlukApp.Common.Models.Queries;

namespace SozlukApp.Common.Models.RequestModels
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
