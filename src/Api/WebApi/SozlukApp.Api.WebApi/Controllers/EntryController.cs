using MediatR;
using Microsoft.AspNetCore.Mvc;
using SozlukApp.Api.Application.Features.Queries.GetEntries;
using SozlukApp.Api.Application.Features.Queries.GetMainPageEntries;
using SozlukAppCommon.Models.RequestModels;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace SozlukApp.Api.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntryController : BaseController
    {
        private readonly IMediator mediator;

        public EntryController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetEntries([FromQuery] GetEntriesQuery query)
        {
            var result = await mediator.Send(query);

            return Ok(result);
        }

        [HttpGet]
        [Route("MainEntries")]
        public async Task<IActionResult> GetMainEntries(int page, int pageSize)
        {
            var result = await mediator.Send(new GetMainPageEntriesQuery(page, pageSize, UserId)); // from base controller 

            return Ok(result);
        }

        [HttpPost]
        [Route("CreateEntry")]
        public async Task<IActionResult> CreateEntry([FromBody] CreateEntryCommand command)
        {
            if (!command.CreatedById.HasValue)
                command.CreatedById = UserId;

            var result = await mediator.Send(command);
            return Ok(result);
        }

        [HttpPost]
        [Route("CreateEntryComment")]
        public async Task<IActionResult> CreateEntryComment([FromBody] CreateEntryCommentCommand command)
        {
            if (!command.CreatedById.HasValue)
                command.CreatedById = UserId;

            var result = await mediator.Send(command);
            return Ok(result);
        }



    }
}
