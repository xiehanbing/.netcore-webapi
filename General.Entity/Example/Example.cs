using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace General.EntityFrameworkCore.Example
{
    /// <summary>
    /// 实例实体
    /// </summary>
    /// table 对应的是数据库的表名 
    public class Example
    {
        /// <summary>
        /// 如果是主键 需要加这个
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// 如果实体名和 数据库中的名称不一致 可贴 column 
        /// </summary>
        [Column("Name")]
        public string Name { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
    }
}