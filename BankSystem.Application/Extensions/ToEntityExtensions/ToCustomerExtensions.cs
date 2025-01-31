using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankSystem.Application.CQRS.CustomerService.Commands.Create;
using BankSystem.Application.CQRS.CustomerService.Commands.Update;
using BankSystem.Application.Models.CustomerModel;
using BankSystem.Domain.Extensions;
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

        public static Customer ToCustomer(this Customer model, CustomerUpdateCommand request)
        {
            model.Name = request.Name;
            model.NationalCode = request.NationalCode;
            model.Address = request.Address;
            model.BirthDate = request.BirthDate;
            model.PostCode = request.PostCode;
            model.PostCode = request.PostCode;
            model.UserType = request.UserType;

            return model;
        }

        public static CustomerSearchModel ToSearchModel(this Customer model, BankTransaction? action)
        {
            var result = new CustomerSearchModel
            {
                Id = model.Id,
                Name = model.Name,
                NationalCode = model.NationalCode,
                Address = model.Address,
                BirthDate = model.BirthDate,
                PersianBirtdate = model.PersianBirtdate,
                UserTypeTitle = model.UserType.ToEnumTitle(),
                PhoneNumber = model.PhoneNumber,
                PostCode = model.PostCode,
                AccountNumber = model.Account.AccountNumber,
                AccountBalance = model.Account.AccountBalance,
                AccountStatusTitle = model.Account.AccountStatus.ToEnumTitle(),

            };
            if (action != null)
            {
                result.TransactionNumber = action.TransactionNumber;
                result.TransactionType = action.TransactionEnum.ToEnumTitle();
                result.TransactionValue = action.TransactionValue;
            }

            return result;

        }
    }
}
