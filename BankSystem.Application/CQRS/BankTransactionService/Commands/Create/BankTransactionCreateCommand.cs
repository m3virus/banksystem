﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankSystem.Domain.Models.Base;
using BankSystem.Domain.Models.Entities;
using BankSystem.Domain.Models.Enums;
using MediatR;

namespace BankSystem.Application.CQRS.BankTransactionService.Commands.Create
{
    public class BankTransactionCreateCommand : IRequest<BaseResponse>
    {
        public double TransactionValue { get; set; }
        public BankTransactionEnum TransactionEnum { get; set; }
        public Guid? OriginAccountId { get; set; }
        public Guid? DestinationAccountId { get; set; }

    }
}
