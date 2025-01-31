using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankSystem.Domain.Models.Base;
using BankSystem.Domain.Models.Enums;
using MediatR;

namespace BankSystem.Application.CQRS.AccountService.Commands.Update
{
    public record AccountUpdateCommand(Guid Id, AccountStatusEnum status) : IRequest<BaseResponse>;

}
