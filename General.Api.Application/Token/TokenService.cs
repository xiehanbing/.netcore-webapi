using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace General.Api.Application.Token
{
    /// <summary>
    /// token 接口
    /// </summary>
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<bool> Get(string account, string password)
        {
            if (account.Equals("xiehanbing") && password.Equals("xiehanbing"))
            {
                return true;
            }

            return false;
        }
    }
}