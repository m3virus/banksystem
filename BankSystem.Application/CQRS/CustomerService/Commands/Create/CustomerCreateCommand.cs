using BankSystem.Domain.Models.Base;
using BankSystem.Domain.Models.Enums;
using MediatR;

namespace BankSystem.Application.CQRS.CustomerService.Commands.Create
{
    public class CustomerCreateCommand:IRequest<BaseResponse<string>>
    {
        public string Name { get; set; }
        public string NationalCode { get; set; }
        public DateTime BirthDate { get; set; }
        public UserTypeEnum UserType { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string PostCode { get; set; }
    }
}
