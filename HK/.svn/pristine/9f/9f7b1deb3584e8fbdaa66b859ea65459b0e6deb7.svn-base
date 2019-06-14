using System.Collections.Generic;
using System.ComponentModel;

namespace General.Api.Application.Door.Dto
{
    /// <summary>
    /// 门禁权限更新model
    /// </summary>
    public class DoorAuthAddDto
    {
        /// <summary>
        /// 下载任务唯一标识
        /// </summary>
        public string TaskId { get; set; }
        /// <summary>
        /// 资源对象
        /// </summary>
        public List<ResourceInfo> ResourceInfos { get; set; }
        /// <summary>
        /// 人员信息
        /// </summary>
        public List<PersonInfo> PersonInfos { get; set; }
        /// <summary>
        /// 任务类型
        /// </summary>
        public DoorAuthTaskType TaskType { get; set; }

    }
    /// <summary>
    /// 门禁点权限任务类型
    /// </summary>
    public enum DoorAuthTaskType
    {
        /// <summary>
        /// 卡片
        /// </summary>
        [Description("卡片")]
        Card = 1,
        /// <summary>
        /// 指纹
        /// </summary>
        [Description("指纹")]
        Finger = 2,
        /// <summary>
        /// 人脸
        /// </summary>
        [Description("人脸")]
        Face = 4,

        //todo 人脸和指纹权限的下载，需要先下载卡片的权限
    }
}