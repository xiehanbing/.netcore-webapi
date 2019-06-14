using System.IO;
using System.Runtime.CompilerServices;
using log4net;
using log4net.Config;
using log4net.Core;
using log4net.Repository;
using log4net.Repository.Hierarchy;

namespace General.Log
{
    /// <summary>
    /// log4net 的 上下文
    /// </summary>
    public class LogContext
    {
        /// <summary>
        /// log仓储接口
        /// </summary>
        private static ILoggerRepository _loggerRepository;
        /// <summary>
        /// 日志接口
        /// </summary>
        private static ILog _log;
        /// <summary>
        /// 日志实现类
        /// </summary>
        public static ILog Log
        {
            get
            {
                if (_log != null) return _log;
                _log = log4net.LogManager.GetLogger(LoggerRepository.Name, LogConsts.Log4NetLoggerName);
                return _log;
            }
        }
        ///// <summary>
        ///// 数据库日志接口
        ///// </summary>
        //private static ILog _dbLog;
        ///// <summary>
        ///// 数据库日志实现类
        ///// </summary>
        //public static ILog DbLog
        //{
        //    get
        //    {
        //        if (_dbLog != null) return _dbLog;
        //        _dbLog = log4net.LogManager.GetLogger(LoggerRepository.Name, LogConsts.Log4NetDbLoggerName);
        //        return _dbLog;
        //    }
        //}
        /// <summary>
        /// log4net 实例
        /// </summary>
        public static ILoggerRepository LoggerRepository
        {
            get
            {
                if (_loggerRepository != null)
                {
                    return _loggerRepository;
                }

                _loggerRepository = LoggerManager.CreateRepository(LogConsts.Log4NetRepositoryName);
                XmlConfigurator.Configure(_loggerRepository, new FileInfo(LogConsts.Log4NetConfigPath));
                return _loggerRepository;
            }
        }


        /// <summary>
        /// 初始化 线程安全
        /// </summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void Initialize()
        {
            _loggerRepository = LoggerManager.CreateRepository(LogConsts.Log4NetRepositoryName);
            XmlConfigurator.Configure(_loggerRepository, new FileInfo(LogConsts.Log4NetConfigPath));
        }

        #region set sqlconfig log4net

        //public static void Configration(string connString, string loggerName, string appenderName = "ADONetAppender")
        //{
        //    if (log4net.LogManager.GetRepository(LoggerRepository.Name) is Hierarchy hie)
        //    {
        //        AdoNetAppender adoNetAppender =
        //            (AdoNetAppender)hie.GetLogger(loggerName, hie.LoggerFactory).GetAppender(appenderName);
        //        if (adoNetAppender != null)
        //        {
        //            adoNetAppender.ConnectionString = connString;
        //            adoNetAppender.ActivateOptions();
        //        }
        //    }
        //}

        #endregion
    }
}