using MediatR;

namespace SozlukAppCommon.Models.RequestModels
{
    public class UpdateUserCommand: IRequest<Guid> // return which updated user's guid
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string UserName { get; set; }
    }
}
