using System;
using System.Threading.Tasks;
using CleanArchitecture.Application.UseCases.X.Commands.Process;
using CleanArchitecture.Application.UseCases.X.Queries.Get;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.API.Controllers
{
    [ApiController]
    [Route("x")]
    public class XController : ControllerBase
    {
        private readonly IMediator _mediator;

        public XController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("{id}/process")]
        public async Task<IActionResult> Process(Guid id)
        {
            return Ok(await _mediator.Send(new ProcessCommand(id)));
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            return Ok(await _mediator.Send(new GetQuery(id)));
        }
    }
}