using System;

namespace General.Api.Framework.Token
{
    public class JwtOption
    {
        public  string Issuer { get; set; } = "general";
        public  string Audience { get; set; } = "general";
        public  TimeSpan Expiration { get; set; } = TimeSpan.FromMinutes(50);
        public  string SecurityKey { get; set; }
        public  string Name { get; set; }
    }
}