using System.Data;
using System.Data.SqlClient;

namespace General.EntityFrameworkCore.Dapper
{
    /// <summary>
    /// dapper 帮助类
    /// </summary>
    public class DapperHelper
    {
        /// 数据库连接名
        private static string _connection = string.Empty;

        ///获取连接名        
        private static string Connection => _connection;

        /// 返回连接实例        
        private static IDbConnection _dbConnection = null;

        /// 静态变量保存类的实例        
        private static DapperHelper _uniqueInstance;

        /// 定义一个标识确保线程同步        
        private static readonly object Locker = new object();
        /// <summary>
        /// 私有构造方法，使外界不能创建该类的实例，以便实现单例模式
        /// </summary>
        public DapperHelper()
        {
            // 这里为了方便演示直接写的字符串，实例项目中可以将连接字符串放在配置文件中，再进行读取。
            _connection = @"server=.;uid=sa;pwd=sasasa;database=Dapper";
        }

        /// <summary>
        /// 获取实例，这里为单例模式，保证只存在一个实例
        /// </summary>
        /// <returns></returns>
        public static DapperHelper GetInstance()
        {
            // 双重锁定实现单例模式，在外层加个判空条件主要是为了减少加锁、释放锁的不必要的损耗
            if (_uniqueInstance == null)
            {
                lock (Locker)
                {
                    if (_uniqueInstance == null)
                    {
                        _uniqueInstance = new DapperHelper();
                    }
                }
            }
            return _uniqueInstance;
        }


        /// <summary>
        /// 创建数据库连接对象并打开链接
        /// </summary>
        /// <returns></returns>
        public static IDbConnection OpenCurrentDbConnection()
        {
            if (_dbConnection == null)
            {
                _dbConnection = new SqlConnection(Connection);
            }
            //判断连接状态
            if (_dbConnection.State == ConnectionState.Closed)
            {
                _dbConnection.Open();
            }
            return _dbConnection;
        }
    }
}