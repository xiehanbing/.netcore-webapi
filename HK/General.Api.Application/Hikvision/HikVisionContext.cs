using General.Core;
using Microsoft.Extensions.Configuration;

namespace General.Api.Application.Hikvision
{
    /// <summary>
    /// 海康威视 上下文
    /// </summary>
    public class HikVisionContext
    {
        /// <summary>
        /// construct
        /// </summary>
        public HikVisionContext(IConfiguration configuration)
        {
            HikVisionBaseUrl = configuration[HikVisionBaseApiName];
        }
     
        /// <summary>
        /// DoorControlBaseApiName
        /// </summary>
        public const string DoorControlBaseApiName = "hikvisionUrl:doorControl";
        /// <summary>
        /// UserBaseApiName
        /// </summary>
        public const string UserBaseApiName = "";
        /// <summary>
        /// HikVisionBaseApiName
        /// </summary>
        public const string HikVisionBaseApiName = "hikvisionUrl:baseUrl";
        /// <summary>
        /// HikVisionBaseUrl
        /// </summary>

        public static string HikVisionBaseUrl = EngineContext.CurrentEngin.Resolve<IConfiguration>()[HikVisionBaseApiName];
    }
}