using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankSystem.Domain.Models.Entities;
using BankSystem.Infrastructure.CustomException;
using BankSystem.Infrastructure.Options;
using BankSystem.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace BankSystem.Infrastructure.Services
{
    public static class  ChangeTrackingService
    {
        private static UserInfoOption _option;

        public static void Configure(UserInfoOption option)
        {
            _option = option;
        }

        public static ChangeTracking CreateChangeTracking(string entity, string status)
        {

            return new ChangeTracking
            {
                UserName = _option.UserName,
                Entity = entity,
                Status = status
            };
        }
    }
}
