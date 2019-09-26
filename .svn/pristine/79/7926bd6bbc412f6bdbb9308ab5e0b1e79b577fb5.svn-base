using System;
using General.Core.Extension;

namespace General.Api.Application.Door.Dto
{
    /// <summary>
    /// 门禁权限进度
    /// </summary>
    public class DoorAuthTaskProgressResponse
    {
        /// <summary>
        /// 标签 用于区分不同业务组件，建议使用组件标识。只支持1-32个数字和小写字母
        /// </summary>
        public string  TagId { get; set; }
        /// <summary>
        /// 任务id
        /// </summary>
        public string  TaskId { get; set; }
        /// <summary>
        /// 任务类型
        /// </summary>
        public DoorAuthTaskType  TaskType { get; set; }
        /// <summary>
        /// 任务类型描述
        /// </summary>
        public string TaskTypeDesc => typeof(DoorAuthTaskType).GetEnumDescription(TaskType.GetHashCode());
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; }
        /// <summary>
        /// 当前进度
        /// </summary>
        public double TotalPercent { get; set; }
        /// <summary>
        /// 下载剩余时间 下载剩余时间，单位秒
        /// </summary>
        public long LeftTime { get; set; }
        /// <summary>
        /// 下载总用时 单位秒
        /// </summary>
        public long ConsimeTime { get; set; }
        /// <summary>
        /// 是否下载完成
        /// </summary>
        public bool IsDownloadFinished { get; set; }

        //todo  还有剩余的实体对象 详情看https://open.hikvision.com/docs/e5f80c56b7b503e3e12e2e99711ca3d0#fd3de11f  下载信息查询
    }
}