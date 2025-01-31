using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankSystem.Application.Models.CustomerModel;
using BankSystem.Domain.Models.Base;
using BankSystem.Domain.Models.Enums;
using MediatR;

namespace BankSystem.Application.CQRS.BankTransactionService.Queries
{
    public class BankTransactionGetQuery:IRequest<BaseResponse<List<BankTransactionSearchModel>>>
    {
        public string AccountNumber { get; set; }
        public DateTime? StartingDate { get; set; } = null;
        public DateTime? EndDate { get; set; } = null;
        public double? StartingAmount { get; set; } = null;
        public double? EndingAmount { get; set; } = null;
        public List<BankTransactionEnum?> BankTransaction { get; set; } = new();
        public int? Take { get; set; } = null;
        public int? page { get; set; } = null;
        public int? Weight { get; set; } = null;
    }
}
