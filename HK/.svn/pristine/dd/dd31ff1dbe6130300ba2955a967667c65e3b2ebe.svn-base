using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using General.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace General.Api.Framework.Token
{
    /// <summary>
    /// token 处理
    /// </summary>
    public class TokenHandle
    {
        /// <summary>
        /// 生成token
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public static string CreateToken(IConfiguration configuration, TokenRequest request)
        {
            //验证通过 生成token
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, request.Account),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sid,request.Account),
                new Claim("ruser",request.Account)
                //new Claim("age",19.ToString()), 
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:SecurityKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(issuer: configuration["Jwt:Issuer"],
                audience: configuration["Jwt:Audience"],
                claims: claims,              
                notBefore:DateTime.Now,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}