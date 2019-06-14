using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace General.Core.Data
{
    /// <summary>
    /// 仓储接口
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// db 上下文
        /// </summary>
        DbContext DbContext { get; }
        /// <summary>
        /// dbset 实体
        /// </summary>
        DbSet<TEntity> Entities { get; }
        /// <summary>
        /// 表
        /// </summary>
        IQueryable<TEntity> Table { get; }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        TEntity GetById(object id);
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        Task<TEntity> GetByIdAsync(object id);
        /// <summary>
        /// 插入实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="isSave">是否保存</param>
        /// <returns></returns>
        object Insert(TEntity entity, bool isSave = true);
        /// <summary>
        /// 插入实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="isSave">是否保存</param>
        /// <returns></returns>
        Task<object> InsertAsync(TEntity entity, bool isSave = true);
        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="isSave">是否保存</param>
        /// <returns></returns>
        int Update(TEntity entity, bool isSave = true);
        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="isSave">是否保存</param>
        /// <returns></returns>
        Task<int> UpdateAsync(TEntity entity, bool isSave = true);
        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="isSave">是否保存</param>
        /// <returns></returns>
        int Delete(TEntity entity, bool isSave = true);
        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="isSave">是否保存</param>
        /// <returns></returns>
        Task<int> DeleteAsync(TEntity entity, bool isSave = true);
    }
}