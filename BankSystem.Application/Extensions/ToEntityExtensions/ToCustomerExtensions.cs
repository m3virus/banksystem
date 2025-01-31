using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankSystem.Application.CQRS.CustomerService.Commands.Create;
using BankSystem.Domain.Models.Entities;
using BankSystem.Domain.Models.Enums;

namespace BankSystem.Application.Extensions.ToEntityExtensions
{
    public static class ToCustomerExtensions
    {
        public static Customer ToCustomer(this CustomerCreateCommand command)
        {
            var result = new Customer
            {
                Name = command.Name,
                BirthDate = command.BirthDate,
                Address = command.Address,
                NationalCode = command.NationalCode,
                PhoneNumber = command.PhoneNumber,
                PostCode = command.PostCode,
                UserType = command.UserType,
            };
            return result;
    }
}
}
