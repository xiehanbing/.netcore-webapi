using System.Linq;
using System.Threading.Tasks;
using General.Core.Data;
using Microsoft.EntityFrameworkCore;

namespace General.EntityFrameworkCore
{
    /// <summary>
    /// 仓储实现
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class EfRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly GeneralDbContext _dbContext;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbContext"></param>
        public EfRepository(GeneralDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        /// <summary>
        /// dbcontext
        /// </summary>
        public DbContext DbContext => _dbContext;
        /// <summary>
        /// dbset
        /// </summary>
        public DbSet<TEntity> Entities => _dbContext.Set<TEntity>();

        /// <summary>
        /// 只做查询
        /// </summary>
        public IQueryable<TEntity> Table => Entities;
        /// <summary>
        /// 查询 根据id主键
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TEntity GetById(object id)
        {
            return _dbContext.Set<TEntity>().Find(id);
        }
        /// <summary>
        /// 异步查询
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public async Task<TEntity> GetByIdAsync(object id)
        {
            return await _dbContext.Set<TEntity>().FindAsync(id);
        }

        /// <summary>
        /// 插入 返回 插入的实体对象
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="isSave">是否保存</param>
        /// <returns></returns>
        public object Insert(TEntity entity, bool isSave = true)
        {
            var data = Entities.Add(entity);
            if (isSave)
            {
                _dbContext.SaveChanges();
            }

            return data;
        }
        /// <summary>
        /// 异步插入实体 
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="isSave">是否保存</param>
        /// <returns></returns>
        public async Task<object> InsertAsync(TEntity entity, bool isSave = true)
        {
            var data = Entities.Add(entity);
            if (isSave)
            {
                await _dbContext.SaveChangesAsync();
            }

            return data;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="isSave">是否保存</param>
        /// <returns></returns>
        public int Update(TEntity entity, bool isSave = true)
        {
            var success = 0;
            if (isSave)
            {
                success = _dbContext.SaveChanges();
            }
            return success;
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="isSave">是否保存</param>
        /// <returns></returns>
        public async Task<int> UpdateAsync(TEntity entity, bool isSave = true)
        {
            var success = 0;
            if (isSave)
            {
                success = await _dbContext.SaveChangesAsync();
            }
            return success;
        }

        /// <summary>
        /// 移除
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="isSave">是否保存</param>
        /// <returns></returns>
        public int Delete(TEntity entity, bool isSave = true)
        {
            Entities.Remove(entity);
            return _dbContext.SaveChanges();
        }
        /// <summary>
        /// 移除
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="isSave">是否保存</param>
        /// <returns></returns>
        public async Task<int> DeleteAsync(TEntity entity, bool isSave = true)
        {
            Entities.Remove(entity);
            return await _dbContext.SaveChangesAsync();
        }
    }
}