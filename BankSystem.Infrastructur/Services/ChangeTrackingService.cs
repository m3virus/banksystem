using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankSystem.Domain.Models.Entities;
using BankSystem.Infrastructure.CustomException;
using BankSystem.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace BankSystem.Infrastructure.Services
{
    internal static class ChangeTrackingService
    {
        public static ChangeTracking CreateChangeTracking(string entity, string status, Guid userId)
        {
            var model = new ChangeTracking
            {
                UserId = userId,
                Entity = entity,
                Status = status
            };

            return model;
        }
    }
}
