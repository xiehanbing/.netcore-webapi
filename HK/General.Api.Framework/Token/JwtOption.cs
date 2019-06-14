using System;

namespace General.Api.Framework.Token
{
    /// <summary>
    /// JwtOption
    /// </summary>
    public class JwtOption
    {
        /// <summary>
        /// Issuer
        /// </summary>
        public string Issuer { get; set; } = "general";
        /// <summary>
        /// Audience
        /// </summary>
        public string Audience { get; set; } = "general";
        /// <summary>
        /// Expiration
        /// </summary>
        public TimeSpan Expiration { get; set; } = TimeSpan.FromMinutes(50);
        /// <summary>
        /// SecurityKey
        /// </summary>
        public string SecurityKey { get; set; }
        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }
    }
}