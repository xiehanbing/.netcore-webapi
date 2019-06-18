using System.Data;
using System.Linq;
using General.EntityFrameworkCore.Dapper;
using General.EntityFrameworkCore.Log;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Infrastructure.Internal;

namespace General.EntityFrameworkCore
{
    /// <summary>
    /// dbcontext
    /// </summary>
    public class GeneralDbContext : DbContext
    {
        public GeneralDbContext(DbContextOptions<GeneralDbContext> options) : base(options)
        {

        }
        //add dbset 

        //如果这里 examples 的名字 和 数据库的表明是一样的  那么example 中不需要加table 
        public DbSet<Example.Example> Examples { get; set; }

        public DbSet<User.User> Users { get; set; }

        public DbSet<ApiLog> ApiLogs { get; set; }


        public DbSet<ApiAuthUser.ApiAuthUser> ApiAuthUsers { get; set; }
        /// <summary>
        /// ApiAuthUserTokens
        /// </summary>
        public DbSet<ApiAuthUser.ApiAuthUserToken> ApiAuthUserTokens { get; set; }
        //public DbSet<TableLog> TableLogs { get; set; }
    }
}