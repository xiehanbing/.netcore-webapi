using System;
using System.Linq;
using System.Threading.Tasks;
using General.Core.Data;
using General.Core.Extension;
using Microsoft.EntityFrameworkCore;

namespace General.Api.Core.ApiAuthUser
{
    public class ApiAuthUserDao : IApiAuthUserDao
    {
        private readonly IRepository<EntityFrameworkCore.ApiAuthUser.ApiAuthUser> _apiUseRepository;

        public ApiAuthUserDao(IRepository<EntityFrameworkCore.ApiAuthUser.ApiAuthUser> apiUseRepository)
        {
            _apiUseRepository = apiUseRepository;
        }
        /// <summary>
        /// <see cref="IApiAuthUserDao.VerifyUser(string,string)"/>
        /// </summary>
        public async Task<bool> VerifyUser(string account, string password)
        {
            var data = await _apiUseRepository.Entities.FirstOrDefaultAsync(o =>
                 o.Account.Trim().Equals(account.Trim()) && o.Password.Trim().Equals(password.Trim()) && !o.IsDeleted);            
            if (data != null) return true;
            return false;
        }
    }
}