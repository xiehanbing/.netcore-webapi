using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using General.Core;
using General.Core.Data;
using General.Core.Extension;
using General.EntityFrameworkCore;
using General.EntityFrameworkCore.Log;
using Microsoft.Extensions.Configuration;

namespace General.Api.Core.Log
{
    /// <summary>
    /// LogManage
    /// </summary>
    public class LogManage
    {
        /// <summary>
        /// 记录日志
        /// </summary>
        public static void ApiLog(ApiLog log)
        {

            log.CreationTime = DateTime.Now;
            log.OprNo = log.OprNo ?? "";
            log.LogType = LogTypeEnum.ApiRequest.GetHashCode().ToString();
            log.LogName = typeof(LogTypeEnum).GetEnumDescription(LogTypeEnum.ApiRequest.GetHashCode());
            using (SqlConnection conn = new SqlConnection(LogContext.ConnectionString))
            {
                if (conn.State != System.Data.ConnectionState.Open)
                {
                    conn.Open();
                }

                conn.Execute(@"  INSERT INTO dbo.SysRequestLog
                ( ConfirmNo ,
                  ModelName ,
                  RequestContext ,
                  ResoponseContext ,
                  CreationTime ,
                  OprNo ,
                  LogType ,
                  LogName
                )
        VALUES  ( @confirmNo , -- ConfirmNo - nvarchar(500)
                  @modelName , -- ModelName - nvarchar(500)
                  @request , -- RequestContext - text
                  @response , -- ResoponseContext - text
                  @date , -- CreationTime - datetime
                  @oprNo , -- OprNo - nvarchar(50)
                  @logType , -- LogType - varchar(50)
                  @logName  -- LogName - nvarchar(500)
                )", new
                {
                    confirmNo = log.ConfirmNo,
                    modelName = log.ModelName,
                    request = log.RequestContext,
                    response = log.ResponseContext,
                    date = log.CreationTime,
                    oprNo = log.OprNo,
                    logType = log.LogType,
                    logName = log.LogName
                });

            }
        }
        /// <summary>
        /// ResourceLog
        /// </summary>
        /// <param name="log"></param>
        public static void ResourceLog(ApiLog log)
        {
            log.CreationTime = DateTime.Now;
            log.OprNo = log.OprNo ?? "";
            log.LogType = LogTypeEnum.Resource.GetHashCode().ToString();
            log.LogName = typeof(LogTypeEnum).GetEnumDescription(LogTypeEnum.Resource.GetHashCode());
            Task.Run(() =>
            {
                using (SqlConnection conn = new SqlConnection(LogContext.ConnectionString))
                {
                    if (conn.State != System.Data.ConnectionState.Open)
                    {
                        conn.Open();
                    }

                    conn.Execute(@"  INSERT INTO dbo.SysRequestLog
                ( ConfirmNo ,
                  ModelName ,
                  RequestContext ,
                  ResoponseContext ,
                  CreationTime ,
                  OprNo ,
                  LogType ,
                  LogName
                )
        VALUES  ( @confirmNo , -- ConfirmNo - nvarchar(500)
                  @modelName , -- ModelName - nvarchar(500)
                  @request , -- RequestContext - text
                  @response , -- ResoponseContext - text
                  @date , -- CreationTime - datetime
                  @oprNo , -- OprNo - nvarchar(50)
                  @logType , -- LogType - varchar(50)
                  @logName  -- LogName - nvarchar(500)
                )", new
                    {
                        confirmNo = log.ConfirmNo,
                        modelName = log.ModelName,
                        request = log.RequestContext,
                        response = log.ResponseContext,
                        date = log.CreationTime,
                        oprNo = log.OprNo,
                        logType = log.LogType,
                        logName = log.LogName
                    });

                }
            });
        }
        /// <summary>
        /// ExceptionLog
        /// </summary>
        /// <param name="log"></param>
        public static void ExceptionLog(ApiLog log)
        {
            log.CreationTime = DateTime.Now;
            log.OprNo = log.OprNo ?? "";
            log.LogType = LogTypeEnum.Exception.GetHashCode().ToString();
            log.LogName = typeof(LogTypeEnum).GetEnumDescription(LogTypeEnum.Exception.GetHashCode());
            Task.Run(() =>
            {
                using (SqlConnection conn = new SqlConnection(LogContext.ConnectionString))
                {
                    if (conn.State != System.Data.ConnectionState.Open)
                    {
                        conn.Open();
                    }

                    conn.Execute(@"  INSERT INTO dbo.SysRequestLog
                ( ConfirmNo ,
                  ModelName ,
                  RequestContext ,
                  ResoponseContext ,
                  CreationTime ,
                  OprNo ,
                  LogType ,
                  LogName
                )
        VALUES  ( @confirmNo , -- ConfirmNo - nvarchar(500)
                  @modelName , -- ModelName - nvarchar(500)
                  @request , -- RequestContext - text
                  @response , -- ResoponseContext - text
                  @date , -- CreationTime - datetime
                  @oprNo , -- OprNo - nvarchar(50)
                  @logType , -- LogType - varchar(50)
                  @logName  -- LogName - nvarchar(500)
                )", new
                    {
                        confirmNo = log.ConfirmNo,
                        modelName = log.ModelName,
                        request = log.RequestContext,
                        response = log.ResponseContext,
                        date = log.CreationTime,
                        oprNo = log.OprNo,
                        logType = log.LogType,
                        logName = log.LogName
                    });

                }
            });

        }
        /// <summary>
        /// HttpClientLog
        /// </summary>
        /// <param name="log"></param>
        public static void HttpClientLog(ApiLog log)
        {
            log.CreationTime = DateTime.Now;
            log.OprNo = log.OprNo ?? "";
            log.LogType = LogTypeEnum.HttpClient.GetHashCode().ToString();
            log.LogName = typeof(LogTypeEnum).GetEnumDescription(LogTypeEnum.HttpClient.GetHashCode());
            Task.Run(() =>
            {
                using (SqlConnection conn = new SqlConnection(LogContext.ConnectionString))
                {
                    if (conn.State != System.Data.ConnectionState.Open)
                    {
                        conn.Open();
                    }

                    conn.Execute(@"  INSERT INTO dbo.SysRequestLog
                ( ConfirmNo ,
                  ModelName ,
                  RequestContext ,
                  ResoponseContext ,
                  CreationTime ,
                  OprNo ,
                  LogType ,
                  LogName
                )
        VALUES  ( @confirmNo , -- ConfirmNo - nvarchar(500)
                  @modelName , -- ModelName - nvarchar(500)
                  @request , -- RequestContext - text
                  @response , -- ResoponseContext - text
                  @date , -- CreationTime - datetime
                  @oprNo , -- OprNo - nvarchar(50)
                  @logType , -- LogType - varchar(50)
                  @logName  -- LogName - nvarchar(500)
                )", new
                    {
                        confirmNo = log.ConfirmNo,
                        modelName = log.ModelName,
                        request = log.RequestContext,
                        response = log.ResponseContext,
                        date = log.CreationTime,
                        oprNo = log.OprNo,
                        logType = log.LogType,
                        logName = log.LogName
                    });
                }
            });
        }

    }
}