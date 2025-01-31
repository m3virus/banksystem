using BankSystem.Domain.Models.Base;
using MediatR;

namespace BankSystem.Application.CQRS.CustomerService.Commands.Delete
{
    public record CustomerDeleteCommand(Guid Id) : IRequest<BaseResponse>;
}
