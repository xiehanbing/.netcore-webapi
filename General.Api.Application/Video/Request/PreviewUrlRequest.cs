using System;
using System.Collections.Generic;
using System.Text;

namespace General.Api.Application.Video.Request
{
    /// <summary>
    /// 预览流
    /// </summary>
    public class PreviewUrlRequest
    {
        /// <summary>
        /// 监控点唯一标识  根据【监控点信息】接口获取返回参数cameraIndexCode。
        /// </summary>
        public string CameraIndexCode { get; set; }
        /// <summary>
        /// 码流类型 0:主码流，1:子码流，2:第三码流，参数不填，默认为主码流
        /// </summary>
        public int StreamType { get; set; }
        /// <summary>
        /// 取流协议（应用层协议）  “rtsp”:RTSP协议,“rtmp”:RTMP协议,“hls”:HLS协议（HLS协议只支持海康SDK协议、EHOME协议、GB28181协议、ONVIF协议接入的设备；只支持H264视频编码和AAC音频编码）,参数不填，默认为RTSP协议
        /// </summary>
        public string Protocol { get; set; }
        /// <summary>
        /// 传输协议（传输层协议）  	0:UDP，1:TCP，默认是TCP，注：GB28181 2011版本接入设备的预览只支持UDP传输协议，GB28181 2016版本接入设备的预览支持UDP和TCP传输协议
        /// </summary>
        public int Transmode { get; set; }
        /// <summary>
        /// 标识扩展内容，格式：key=value， 调用方根据其播放控件支持的解码格式选择相应的封装类型；
        /// </summary>
        public string Expand { get; set; }
    }
}
