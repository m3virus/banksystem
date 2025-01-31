using BankSystem.Application.CQRS.CustomerService.Commands.Create;
using BankSystem.Application.CQRS.CustomerService.Commands.Update;
using BankSystem.Application.CQRS.CustomerService.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankSystem.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> GetCustomer(CustomerGetQuery request)
            => Ok(await _mediator.Send(request));

        [HttpPost]
        public async Task<IActionResult> CreateCustomer(CustomerCreateCommand request)
            => Ok(await _mediator.Send(request));

        [HttpPut]
        public async Task<IActionResult> UpdateCustomer(CustomerUpdateCommand request)
            => Ok(await _mediator.Send(request));
    }
}
