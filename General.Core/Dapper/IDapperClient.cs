using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace General.Core.Dapper
{
    /// <summary>
    /// dapper client 
    /// </summary>
    public interface IDapperClient<T> where T : class
    {
        /// <summary>
        /// dbconnection 
        /// </summary>
        IDbConnection Connection { get; }
        /// <summary>
        /// 查询
        /// </summary>
        /// <typeparam name="T">数据列表</typeparam>
        /// <param name="strSql"></param>
        /// <returns></returns>
        List<T> Query(string strSql);
        /// <summary>
        /// 查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strSql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        List<T> Query(string strSql, object param);
        /// <summary>
        /// 查询第一个
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strSql"></param>
        /// <returns></returns>
        T QueryFirst(string strSql);
        /// <summary>
        /// 查询第一个 异步
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strSql"></param>
        /// <returns></returns>
        Task<T> QueryFirstAsync(string strSql);
        /// <summary>
        /// 查询第一个
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strSql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        T QueryFirst(string strSql, object param);
        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        int Execute(string strSql, object param);
        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="strProcedure"></param>
        /// <returns></returns>
        int ExecuteStoredProcedure(string strProcedure);
        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="strProcedure"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        int ExecuteStoredProcedure(string strProcedure, object param);
    }
}