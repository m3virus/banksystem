using BankSystem.Domain.Models.Entities;
using BankSystem.Infrastructure.Context;
using BankSystem.Infrastructure.IRepository;

namespace BankSystem.Infrastructure.Repository
{
    public class CustomerRepository:BaseRepository<Customer>,ICustomerRepository
    {
        public CustomerRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
