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
        public static IRepository<ApiLog> ApiLogRepository ;
        /// <summary>
        /// table log repository
        /// </summary>
        public static IRepository<TableLog> TabLogRepository = EngineContext.CurrentEngin.Resolve<IRepository<TableLog>>();
    }
}