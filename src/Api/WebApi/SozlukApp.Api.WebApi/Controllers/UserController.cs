using MediatR;
using Microsoft.AspNetCore.Mvc;
using SozlukApp.Api.Application.Features.Commands.User.ConfirmEmail;
using SozlukAppCommon.Events.User;
using SozlukAppCommon.Models.RequestModels;

namespace SozlukApp.Api.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly IMediator mediator;

        public UserController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginUserCommand command)
        {
            var result = await mediator.Send(command);

            return Ok(result);
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] CreateUserCommand command)
        {
            var id = await mediator.Send(command);

            return Ok(id);
        }

        [HttpPost]
        [Route("Update")]
        public async Task<IActionResult> Update([FromBody] UpdateUserCommand command)
        {
            var id = await mediator.Send(command);

            return Ok(id);
        }

        [HttpPost]
        [Route("Confirm")]
        public async Task<IActionResult> EmailConfirm(Guid id)
        {
            var guid = await mediator.Send(new ConfirmEmailCommand() { ConfirmationId = id });

            return Ok(guid);
        }

        [HttpPost]
        [Route("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangeUserPasswordCommand command)
        {
            if (!command.UserId.HasValue)
                command.UserId = UserId;

            var isChanged = await mediator.Send(command);

            return Ok(isChanged);
        }
    }
}
