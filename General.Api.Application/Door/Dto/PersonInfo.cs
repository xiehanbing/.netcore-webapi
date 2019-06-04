using System.ComponentModel;

namespace General.Api.Application.Door.Dto
{
    /// <summary>
    /// 人员信息
    /// </summary>
    public class PersonInfo
    {
        /// <summary>
        /// 人员编码
        /// </summary>
        public string  PersonId { get; set; }
        /// <summary>
        /// 操作类型 1添加/修改 2删除
        /// </summary>
        public PersonOperatorType OperatorType { get; set; }
    }
    /// <summary>
    /// 操作类型
    /// </summary>
    public enum PersonOperatorType
    {
        /// <summary>
        /// 添加/修改
        /// </summary>
        [Description("添加/修改")]
        Update=1,
        /// <summary>
        /// 2删除
        /// </summary>
        [Description("删除")]
        Remove =2
    }
}