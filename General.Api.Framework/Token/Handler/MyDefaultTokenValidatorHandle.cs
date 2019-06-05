﻿using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace General.Api.Framework.Token.Handler
{
    /// <summary>
    /// 自定义的jwt token 验证
    /// </summary>
    public class MyDefaultTokenValidatorHandle : ISecurityTokenValidator
    {
        bool ISecurityTokenValidator.CanValidateToken => true;

        int ISecurityTokenValidator.MaximumTokenSizeInBytes { get; set; }
        public bool CanReadToken(string securityToken)
        {
            return true;
        }

        public ClaimsPrincipal ValidateToken(string securityToken, TokenValidationParameters validationParameters,
            out SecurityToken validatedToken)
        {
            validatedToken = null;
            //判断token是否正确
            if (securityToken != "") return null;
            //给identity 赋值
            //给Identity赋值
            var identity = new ClaimsIdentity(JwtBearerDefaults.AuthenticationScheme);
            identity.AddClaim(new Claim("name", "wyt"));
            identity.AddClaim(new Claim(ClaimsIdentity.DefaultRoleClaimType, "admin"));

            var principle = new ClaimsPrincipal(identity);
            return principle;
        }


    }
}