using System.ComponentModel;

namespace General.Core.Dapper
{
    /// <summary>
    /// dapper config 
    /// </summary>
    public class ConnectionConfig
    {
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public string ConnectionString { get; set; }
        /// <summary>
        /// 数据库类型
        /// </summary>
        public DbStoreType DbType { get; set; }
    }
    /// <summary>
    /// 数据库类型枚举
    /// </summary>
    public enum DbStoreType
    {
        [Description("MySql")]
        MySql = 0,
        [Description("SqlServer")]
        SqlServer = 1,
        [Description("Sqlite")]
        Sqlite = 2,
        [Description("Oracle")]
        Oracle = 3
    }

}