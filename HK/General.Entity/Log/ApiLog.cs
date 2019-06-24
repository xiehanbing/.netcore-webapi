﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using General.Core.Extension;

namespace General.EntityFrameworkCore.Log
{
    /// <summary>
    /// apilog 日志
    /// </summary>
    [Table("SysRequestLog")]
    public class ApiLog
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// 标示
        /// </summary>
        public string ConfirmNo { get; set; }
        /// <summary>
        /// 模块名称
        /// </summary>
        public string ModelName { get; set; }
        /// <summary>
        /// 请求内容
        /// </summary>
        public string RequestContext { get; set; }
        /// <summary>
        /// 响应内容
        /// </summary>
        [Column("ResoponseContext")]
        public string ResponseContext { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreationTime { get; set; } = DateTime.Now;
        /// <summary>
        /// 操作人工号
        /// </summary>
        public string OprNo { get; set; }
        /// <summary>
        /// 类型code
        /// </summary>
        public string LogType { get; set; }
        /// <summary>
        /// 类型名称
        /// </summary>
        public string LogName { get; set; }
    }
}