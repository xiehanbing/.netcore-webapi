using System;
using System.Linq;
using System.Threading.Tasks;
using General.Core;
using General.Core.Data;
using General.Core.Extension;
using General.EntityFrameworkCore.ApiAuthUser;
using Microsoft.EntityFrameworkCore;

namespace General.Api.Core.ApiAuthUser
{
    public class ApiAuthUserDao : IApiAuthUserDao
    {
        private readonly IRepository<EntityFrameworkCore.ApiAuthUser.ApiAuthUser> _apiUseRepository;
        private readonly IRepository<EntityFrameworkCore.ApiAuthUser.ApiAuthUserToken> _apiAuthTokenRepository;
        public ApiAuthUserDao(IRepository<EntityFrameworkCore.ApiAuthUser.ApiAuthUser> apiUseRepository,
            IRepository<EntityFrameworkCore.ApiAuthUser.ApiAuthUserToken> apiAuthTokenRepository)
        {
            _apiUseRepository = apiUseRepository;
            _apiAuthTokenRepository = apiAuthTokenRepository;
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
        /// <summary>
        /// <see cref="IApiAuthUserDao.VerifyToken(string,string)"/>
        /// </summary>
        public bool VerifyToken(string token, string account)
        {
            var data = _apiAuthTokenRepository.Entities.FirstOrDefault(o => !o.IsDeleted
                                                                                       && (o.ExpressTime == null ||
                                                                                           (o.ExpressTime.Value >
                                                                                            DateTime.Now)) &&
                                                                                       o.Account.Trim()
                                                                                           .Equals(account.Trim())
                                                                                       && (o.JwtToken.Trim().Equals(token.Trim())));
            return data != null;
        }

        public async Task<bool> AddTokenAsync(string account, string token)
        {
            var removeSuccess = await RemoveTokenAsync(account);
            if (!removeSuccess) throw new MyException(account+"移除历史token 失败");
            var data = await _apiAuthTokenRepository.InsertAsync(new ApiAuthUserToken()
            {
                Account = account,
                JwtToken = token,
                CreationTime = DateTime.Now,
                IsDeleted = false
            });
            return data?.ToString().IsNotNull() ?? false;
        }

        public async Task<bool> UpdateTokenAsync(ApiAuthUserToken token)
        {
            var data = await _apiAuthTokenRepository.UpdateAsync(token);
            return data > 0;
        }

        public async Task<bool> RemoveTokenAsync(string account, string token)
        {
            var data = await _apiAuthTokenRepository.Entities.FirstOrDefaultAsync(o => !o.IsDeleted &&
                                                                                       o.Account.Trim()
                                                                                           .Equals(account.Trim())
                                                                                       && (o.JwtToken.Trim()
                                                                                           .Equals(token.Trim())));

            data.IsDeleted = true;
            data.ExpressTime = DateTime.Now;
            var success = await _apiAuthTokenRepository.DbContext.SaveChangesAsync();
            return success > 0;
        }

        private async Task<bool> RemoveTokenAsync(string account)
        {
            var data = await _apiAuthTokenRepository.Entities.FirstOrDefaultAsync(o => !o.IsDeleted &&
                                                                                       o.Account.Trim()
                                                                                           .Equals(account.Trim()));
            if (data == null) return true;
            data.IsDeleted = true;
            data.ExpressTime = DateTime.Now;
            var success = await _apiAuthTokenRepository.DbContext.SaveChangesAsync();
            return success > 0;
        }
    }
}