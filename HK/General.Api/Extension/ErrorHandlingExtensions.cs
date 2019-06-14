using General.Api.Framework.Middleware;
using Microsoft.AspNetCore.Builder;

namespace General.Api.Extension
{
    /// <summary>
    /// ErrorHandlingExtensions
    /// </summary>
    public static class ErrorHandlingExtensions
    {
        /// <summary>
        /// UseErrorHandling
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseErrorHandling(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErrorHandlingMiddleware>();
        }
    }
}