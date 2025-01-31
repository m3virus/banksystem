using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankSystem.Domain.Models.Base;
using MediatR;

namespace BankSystem.Application.CQRS.AccountService.Queries
{
    public class AccountGetQuery : IRequest<BaseResponse>
    {
    }
}
