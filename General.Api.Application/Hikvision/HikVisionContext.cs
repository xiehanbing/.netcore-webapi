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
        /// artemis网关服务器ip端口
        /// </summary>
        public const string ArtemisHost = "";
        /// <summary>
        /// 秘钥appkey
        /// </summary>
        public const string ArtemisAppKey = "";
        /// <summary>
        /// 秘钥appSecret
        /// </summary>
        public const string ArtemisAppSecret = "";
        /// <summary>
        /// DoorControlBaseApiName
        /// </summary>
        public const string DoorControlBaseApiName = "hikvisionUrl:doorControl";

        public const string UserBaseApiName = "";
        /// <summary>
        /// HikVisionBaseApiName
        /// </summary>
        public const string HikVisionBaseApiName = "hikvisionUrl:baseUrl";

        public static string HikVisionBaseUrl = EngineContext.CurrentEngin.Resolve<IConfiguration>()[HikVisionBaseApiName];
    }
}