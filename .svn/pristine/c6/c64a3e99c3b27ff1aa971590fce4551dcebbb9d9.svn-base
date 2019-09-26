using System;

namespace General.Api.Application.Video.Request
{
    /// <summary>
    /// 获取监控点回放取流url 请求
    /// </summary>
    public class PlaybackUrlRequest
    {
        /// <summary>
        /// 监控点唯一标识
        /// </summary>
        public string  CameraIndexCode { get; set; }
        /// <summary>
        /// 存储类型 0：中心存储，1：设备存储，默认为中心存储
        /// </summary>
        public int  RecordLocation { get; set; }
        /// <summary>
        /// 取流协议（应用层协议） “rtsp”:RTSP协议,“rtmp”:RTMP协议,“hls”:HLS协议（HLS协议只支持海康SDK协议、EHOME协议、ONVIF协议接入的设备；只支持H264视频编码和AAC音频编码；云存储版本要求v2.2.4及以上的2.x版本，或v3.0.5及以上的3.x版本；ISC版本要求v1.2.0版本及以上，需在运管中心-视频联网共享中切换成启动平台内置VOD）,参数不填，默认为RTSP协议
        /// </summary>
        public string  Protocol { get; set; }
        /// <summary>
        /// 传输协议（传输层协议）0:UDP，1:TCP，默认为tcp，在protocol设置为rtsp或者rtmp时有效 注：EHOME协议接入设备的录像回放只支持TCP传输协议
        /// </summary>
        public int  Transmode { get; set; }
        /// <summary>
        /// 开始查询时间
        /// </summary>
        public DateTime BeginTime { get; set; }
        /// <summary>
        /// 结束查询时间
        /// </summary>
        public DateTime EndTime { get; set; }
        /// <summary>
        /// 分页查询id 上一次查询返回的uuid，用于继续查询剩余片段，默认为空字符串
        /// </summary>
        public string Uuid { get; set; }
        /// <summary>
        /// 扩展内容 格式：key=value， 调用方根据其播放控件支持的解码格式选择相应的封装类型
        /// </summary>
        public string  Expand { get; set; }
    }
}