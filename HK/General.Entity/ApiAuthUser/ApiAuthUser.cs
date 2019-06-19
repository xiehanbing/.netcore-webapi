using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace General.EntityFrameworkCore.ApiAuthUser
{
    /// <summary>
    ///api权限账户
    /// </summary>
    [Table("ApiAuthUser")]
    public class ApiAuthUser
    {
        /// <summary>
        /// 自增id
        /// </summary>		
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// 账户
        /// </summary>		
        public string Account { get; set; }
        /// <summary>
        /// 密码-明文
        /// </summary>		
        public string Password { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>		
        public DateTime CreationTime { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>		
        public bool IsDeleted { get; set; }
        /// <summary>
        /// 是否为管理员
        /// </summary>
        public bool IsAdmin { get; set; }
    }


}