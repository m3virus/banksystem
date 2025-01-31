using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankSystem.Domain.Statics;
using FluentValidation;

namespace BankSystem.Application.CQRS.CustomerService.Commands.Update
{
    public class CustomerUpdateCommandValidator:AbstractValidator<CustomerUpdateCommand>
    {
        public CustomerUpdateCommandValidator()
        {
            RuleFor(x => x.PhoneNumber).Matches(RegexStatic.Mobile).WithMessage("شماره تلفن اشتباه وارد شده");
            RuleFor(x => x.PostCode).Length(10).WithMessage("کد پستی اشتباه است");
            RuleFor(x => x.NationalCode).Length(10).WithMessage("کد ملی اشتباه است");
            RuleFor(x => x.Id).NotNull();
        }
    }
}
