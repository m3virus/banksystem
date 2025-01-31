using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankSystem.Domain.Models.Base;
using BankSystem.Domain.Models.Enums;
using BankSystem.Domain.Statics;
using MediatR;

namespace BankSystem.Application.CQRS.CustomerService.Commands.Update
{
    public class CustomerUpdateCommand : IRequest<BaseResponse>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string NationalCode { get; set; }
        public DateTime BirthDate { get; set; }
        public UserTypeEnum UserType { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string PostCode { get; set; }
    }
}
