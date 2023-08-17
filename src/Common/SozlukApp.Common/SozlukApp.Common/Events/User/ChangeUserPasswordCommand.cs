using MediatR;

namespace SozlukApp.Common.Events.User
{
    public class ChangeUserPasswordCommand:IRequest<bool>
    {
        public Guid? UserId { get; set; }
        public string NewPassword { get; set; }
        public string OldPassword { get; set; }

        public ChangeUserPasswordCommand(Guid? userId, string newPassword, string oldPassword)
        {
            UserId = userId;
            NewPassword = newPassword;
            OldPassword = oldPassword;
        }
    }
}
