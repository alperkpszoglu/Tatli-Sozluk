using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SozlukApp.Api.Application.Features.Queries.GetEntries;
using SozlukApp.Api.Application.Features.Queries.GetEntryComments;
using SozlukApp.Api.Application.Features.Queries.GetEntryDetail;
using SozlukApp.Api.Application.Features.Queries.GetMainPageEntries;
using SozlukApp.Api.Application.Features.Queries.GetUserEntries;
using SozlukAppCommon.Models.QueryModels;
using SozlukAppCommon.Models.RequestModels;

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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid entryId)
        {
            var result = await mediator.Send(new GetEntryDetailQuery(entryId, UserId.Value));

            return Ok(result);
        }

        [HttpGet]
        [Route("Comments/{id}")]
        public async Task<IActionResult> GetEntryCommends(Guid entryId, int page, int pageSize)
        {
            var result = await mediator.Send(new GetEntryCommentsQuery(entryId, UserId, page, pageSize));

            return Ok(result);
        }

        [HttpGet]
        [Route("UserEntries")]
        public async Task<IActionResult> GetUserEntries(Guid userId, string userName, int page, int pageSize)
        {
            if (userId != Guid.Empty && !string.IsNullOrEmpty(userName))
                userId = UserId.Value;

            var result = await mediator.Send(new GetUserEntriesQuery(userId, userName, page, pageSize));

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetEntries([FromQuery] GetEntriesQuery query)
        {
            var result = await mediator.Send(query);

            return Ok(result);
        }

        [HttpGet]
        [Route("MainPageEntries")]
        public async Task<IActionResult> GetMainPageEntries(int page, int pageSize)
        {
            var result = await mediator.Send(new GetMainPageEntriesQuery(page, pageSize, UserId)); // from base controller 

            return Ok(result);
        }

        [HttpGet]
        [Route("Search")]
        public async Task<IActionResult> Search([FromQuery] SearchEntryQuery query)
        {
            var result = await mediator.Send(query);

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
