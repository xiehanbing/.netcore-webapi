using System;
using System.Threading.Tasks;
using General.Core;
using General.Core.Data;
using General.Core.Extension;
using General.EntityFrameworkCore.Log;

namespace General.Api.Core.Log
{
    public class LogManage
    {

        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="log"></param>
        public static void ApiLog(ApiLog log)
        {
            log.CreationTime = DateTime.Now;
            log.OprNo = log.OprNo ?? "";
            log.LogType = LogTypeEnum.ApiRequest.GetHashCode().ToString();
            log.LogName = typeof(LogTypeEnum).GetEnumDescription(LogTypeEnum.ApiRequest.GetHashCode());
            Task.Run(() => { LogContext.ApiLogRepository.Insert(log); });

        }
    }
}