using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BankSystem.Application.Models.CustomerModel;
using BankSystem.Domain.Models.Base;
using MediatR;

namespace BankSystem.Application.CQRS.CustomerService.Queries
{
    public class CustomerGetQuery:IRequest<BaseResponse<List<CustomerSearchModel>>>
    {
        public Guid? Id { get; set; } = null;
        public string? Name { get; set; } = null;
        public string? NationalCode { get; set; } = null;
        public string? AccountNumber { get; set; } = null;
    }
}
