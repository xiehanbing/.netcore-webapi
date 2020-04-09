using System.Collections.Generic;
using System.Threading.Tasks;
using General.Api.Application.Hikvision;
using General.Api.Application.Video.Dto;
using General.Api.Application.Video.Request;

namespace General.Api.Application.Video
{
    /// <summary>
    /// 视频服务
    /// </summary>
    public interface IVideoService
    {
        /// <summary>
        /// 获取监控点预览取流URL
        /// </summary>
        /// <param name="cameraIndexCode">监控点唯一标识  根据【监控点信息】接口获取返回参数cameraIndexCode。</param>
        /// <param name="streamType">码流类型 0:主码流，1:子码流，2:第三码流，参数不填，默认为主码流</param>
        /// <param name="protocol">取流协议（应用层协议）  “rtsp”:RTSP协议,“rtmp”:RTMP协议,“hls”:HLS协议（HLS协议只支持海康SDK协议、EHOME协议、GB28181协议、ONVIF协议接入的设备；只支持H264视频编码和AAC音频编码）,参数不填，默认为RTSP协议</param>
        /// <param name="transmode">传输协议（传输层协议）  	0:UDP，1:TCP，默认是TCP，注：GB28181 2011版本接入设备的预览只支持UDP传输协议，GB28181 2016版本接入设备的预览支持UDP和TCP传输协议</param>
        /// <param name="expand">标识扩展内容，格式：key=value， 调用方根据其播放控件支持的解码格式选择相应的封装类型；</param>
        /// <returns></returns>
        Task<string> GetPreviewUrl(string cameraIndexCode, int streamType, string protocol, int transmode,
            string expand);
        /// <summary>
        /// 获取监控点回放取流URL
        /// </summary>
        /// <param name="model">参数</param>
        /// <returns></returns>
        Task<PlayackUrlResponse> GetPlaybackUrlInfo(Request.PlaybackUrlRequest model);
        /// <summary>
        /// 获取预置点列表
        /// </summary>
        /// <param name="cameraIndexCode">监控点唯一标识</param>
        /// <returns></returns>
        Task<ListBaseResponse<PresetsResponse>> GetPresets(string cameraIndexCode);
        /// <summary>
        /// 控制监控点
        /// </summary>
        /// <param name="control">控制参数</param>
        /// <returns></returns>
        Task<bool> Control(ControlModel control);
        /// <summary>
        /// 获取监控点资源
        /// </summary>
        /// <param name="page">页码</param>
        /// <param name="size">页容量</param>
        /// <returns></returns>
        Task<ListBaseResponse<CameraResponse>> GetCameras(int page, int size);
        /// <summary>
        /// 获取监控点资源
        /// </summary>
        /// <param name="request">request 参数</param>
        /// <returns></returns>
        Task<ListBaseResponse<CameraResponse>> GetCameras(CameraRequest request);
    }
}