using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Identity;
using Microsoft.AspNetCore.Http;

namespace BankSystem.Infrastructure.Extensions
{
    public class GetUserInfo
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IUnitOfWork _unitOfWork;

        public GetUserInfo(IHttpContextAccessor contextAccessor, IUnitOfWork unitOfWork)
        {
            _contextAccessor = contextAccessor;
            _unitOfWork = unitOfWork;
        }

        public Guid? GetUserId()
        {
            var userName = _contextAccessor.HttpContext?.User?.Identity?.Name;

            if (userName is null)
            {
                var guid = _unitOfWork.UserRepository.GetQueryable(x => x.UserName == userName).FirstOrDefault()
                    ?.Id;
                return guid;
            }

            return null;
        }
    }
}
