using General.Core;
using General.Core.Data;
using General.EntityFrameworkCore.Log;

namespace General.Api.Core.Log
{
    public class LogContext
    {
        /// <summary>
        /// apilog repository
        /// </summary>
        public static IRepository<ApiLog> ApiLogRepository;

        /// <summary>
        /// Exception apilog repository
        /// </summary>
        public static IRepository<ExceptionApiLog> ExceptionApiLogRepository;

        /// <summary>
        /// Resource apilog repository
        /// </summary>
        public static IRepository<ResourceApiLog> ResourceApiLogRepository;
        /// <summary>
        /// HttpClientApiLogRepository
        /// </summary>
        public static IRepository<HttpClientApiLog> HttpClientApiLogRepository;
        /// <summary>
        /// ConnectionString
        /// </summary>
        public static string ConnectionString = "";
    }
    /// <summary>
    /// apilog enum
    /// </summary>
    public enum ApiLogRepositoryType
    {
        /// <summary>
        /// ApiLog
        /// </summary>
        ApiLog = 1,
        /// <summary>
        /// Exception ApiLog
        /// </summary>
        Exception = 2,
        /// <summary>
        /// Resource ApiLog
        /// </summary>
        Resource = 3,
        /// <summary>
        /// HttpClientLog
        /// </summary>
        HttpClientLog = 4
    }
}