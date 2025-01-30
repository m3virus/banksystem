using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankSystem.Domain.Models.Entities;
using BankSystem.Infrastructure.Context;
using BankSystem.Infrastructure.IRepository;

namespace BankSystem.Infrastructure.Repository
{
    public class BankTransactionRepository:BaseRepository<BankTransaction>,IBankTransactionRepository
    {
        public BankTransactionRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
