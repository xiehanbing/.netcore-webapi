using log4net;
using log4net.Repository;

namespace General.Log
{
    /// <summary>
    /// log manager
    /// </summary>
    public class LogManager : ILogManager
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly ILog _log = LogContext.Log;
        /// <summary>
        /// 
        /// </summary>
        //private readonly ILog _dbLog = LogContext.DbLog;
        /// <summary>
        /// 记录信息
        /// </summary>
        /// <param name="message"></param>
        /// <param name="logManagerType"></param>
        public void Info(object message, LogManagerType logManagerType = LogManagerType.File)
        {
            _log.Info(message);
            //if (logManagerType == LogManagerType.File)
            //    _log.Info(message);
            //else
            //    _dbLog.Info(message);
        }
        /// <summary>
        /// 记录debug
        /// </summary>
        /// <param name="message"></param>
        public void Debug(object message)
        {
            if (_log.IsDebugEnabled)
            {
                _log.Debug(message);
            }
        }
        /// <summary>
        /// 记录错误信息
        /// </summary>
        /// <param name="message"></param>
        public void Error(object message)
        {
            _log.Error(message);
        }
    }
}