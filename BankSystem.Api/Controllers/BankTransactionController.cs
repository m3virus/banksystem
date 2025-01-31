using BankSystem.Application.CQRS.AccountService.Commands.Update;
using BankSystem.Application.CQRS.BankTransactionService.Commands.Create;
using BankSystem.Application.CQRS.BankTransactionService.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankSystem.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BankTransactionController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BankTransactionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create(BankTransactionCreateCommand request)
            => Ok(await _mediator.Send(request));

        [HttpPost]
        public async Task<IActionResult> Search(BankTransactionGetQuery request)
            => Ok(await _mediator.Send(request));
    }
}
