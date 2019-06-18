using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace General.EntityFrameworkCore.ApiAuthUser
{
    [Table("ApiAuthUserToken")]
    public class ApiAuthUserToken
    {
        /// <summary>
        /// 自增
        /// </summary>
        [Key]
        public int  Id { get; set; }
        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime? ExpressTime { get; set; }
        /// <summary>
        /// token 主键
        /// </summary>
        
        public string JwtToken { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreationTime { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDeleted { get; set; }
        /// <summary>
        /// 账号
        /// </summary>
        public string  Account { get; set; }
    }
}