using System;
using System.Collections.Generic;

namespace General.Api.Application.EventRevice.Dto
{

    #region revice with T
    /// <summary>
    /// 事件接收服务 统一接收类
    /// </summary>
    public class EventReciveDto<T>
    {
        /// <summary>
        /// 方法名
        /// </summary>
        public string Method { get; set; }
        /// <summary>
        /// 事件参数信息
        /// </summary>
        public EventReviceParam<T> Params { get; set; }


    }
    /// <summary>
    /// 事件参数信息
    /// </summary>
    public class EventReviceParam<T>
    {
        /// <summary>
        /// 事件类别，如视频事件、门禁事件
        /// </summary>
        public DateTime SendTime { get; set; }
        /// <summary>
        /// 事件类别，如视频事件、门禁事件
        /// </summary>
        public string Ability { get; set; }
        /// <summary>
        /// 指定的事件接收用户列表，用于事件源发起组件指定接收用户，如指定用户接收手动事件、在部分应用中可以设置事件到指定用户接收
        /// </summary>
        public string Uids { get; set; }
        /// <summary>
        /// 事件信息
        /// </summary>
        public List<EventInfo<T>> Events { get; set; }

    }
    /// <summary>
    /// 事件信息
    /// </summary>
    public class EventInfo<T>
    {
        /// <summary>
        /// 事件Id，唯一标识事件的一次发生，同一事件发送多次需要ID相同
        /// </summary>
        public string EventId { get; set; }
        /// <summary>
        /// 事件源编号，物理设备是资源编号
        /// </summary>
        public string SrcIndex { get; set; }
        /// <summary>
        /// 事件源类型
        /// </summary>
        public string SrcType { get; set; }
        /// <summary>
        /// 事件源名称，utf8
        /// </summary>
        public string SrcName { get; set; }
        /// <summary>
        /// 事件类型
        /// </summary>
        public int EventType { get; set; }
        /// <summary>
        /// 事件状态, 0-瞬时 1-开始 2-停止 3-事件脉冲 4-事件联动结果更新 5-异步图片上传
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 事件等级：0-未配置 1-低 2-中 3-高，注意，此处事件等级是指在事件联动中配置的等级
        /// </summary>
        public int EventLvl { get; set; }
        /// <summary>
        /// 脉冲超时时间，一个持续性的事件，上报的间隔
        /// </summary>
        public int TimeOut { get; set; }
        /// <summary>
        /// 事件发生时间
        /// </summary>
        public DateTime HappenTime { get; set; }
        /// <summary>
        /// 事件发生的事件源父设备，无-空字符串
        /// </summary>
        public string SrcParentIndex { get; set; }
        /// <summary>
        /// 事件其它扩展信息 不同类型的事件，扩展信息不同，具体信息可查看具体事件的报文
        /// </summary>
        public T Data { get; set; }
    }
    #endregion

    /// <summary>
    /// 事件接收服务 统一接收类
    /// </summary>
    public class EventReciveDto
    {
        /// <summary>
        /// 方法名
        /// </summary>
        public string Method { get; set; }
        /// <summary>
        /// 事件参数信息
        /// </summary>
        public EventReviceParam Params { get; set; }
    }
    /// <summary>
    /// 事件参数信息
    /// </summary>
    public class EventReviceParam
    {
        /// <summary>
        /// 事件类别，如视频事件、门禁事件
        /// </summary>
        public DateTime SendTime { get; set; }
        /// <summary>
        /// 事件类别，如视频事件、门禁事件
        /// </summary>
        public string Ability { get; set; }
        /// <summary>
        /// 指定的事件接收用户列表，用于事件源发起组件指定接收用户，如指定用户接收手动事件、在部分应用中可以设置事件到指定用户接收
        /// </summary>
        public string Uids { get; set; }
        /// <summary>
        /// 事件信息
        /// </summary>
        public List<EventInfo> Events { get; set; }
    }

    /// <summary>
    /// 事件信息
    /// </summary>
    public class EventInfo
    {
        /// <summary>
        /// 事件Id，唯一标识事件的一次发生，同一事件发送多次需要ID相同
        /// </summary>
        public string EventId { get; set; }
        /// <summary>
        /// 事件源编号，物理设备是资源编号
        /// </summary>
        public string SrcIndex { get; set; }
        /// <summary>
        /// 事件源类型
        /// </summary>
        public string SrcType { get; set; }
        /// <summary>
        /// 事件源名称，utf8
        /// </summary>
        public string SrcName { get; set; }
        /// <summary>
        /// 事件类型
        /// </summary>
        public int EventType { get; set; }
        /// <summary>
        /// 事件状态, 0-瞬时 1-开始 2-停止 3-事件脉冲 4-事件联动结果更新 5-异步图片上传
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 事件等级：0-未配置 1-低 2-中 3-高，注意，此处事件等级是指在事件联动中配置的等级
        /// </summary>
        public int EventLvl { get; set; }
        /// <summary>
        /// 脉冲超时时间，一个持续性的事件，上报的间隔
        /// </summary>
        public int TimeOut { get; set; }
        /// <summary>
        /// 事件发生时间
        /// </summary>
        public DateTime HappenTime { get; set; }
        /// <summary>
        /// 事件发生的事件源父设备，无-空字符串
        /// </summary>
        public string SrcParentIndex { get; set; }
    }

}