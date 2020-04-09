using System;
using System.Threading.Tasks;
using General.Api.Application.Hikvision;
using General.Api.Application.Video.Dto;
using General.Api.Application.Video.Request;
using General.Core;
using General.Core.Extension;
using General.Core.HttpClient;
using General.Core.HttpClient.Extension;
using HttpUtil;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace General.Api.Application.Video
{
    /// <summary>
    /// 视频服务
    /// </summary>
    public class VideoService : IVideoService
    {
        private readonly string _videoUrl = HikVisionContext.HikVisionBaseUrl;
        private readonly ILogger _logger;
        private readonly IHikHttpUtillib _hikHttp;
        public VideoService(ILogger<VideoService> logger, IHikHttpUtillib hikHttp)
        {
            _logger = logger;
            _hikHttp = hikHttp;
        }
        /// <summary>
        /// <see cref="IVideoService.GetPreviewUrl(string,int,string,int,string)"/>
        /// </summary>
        public async Task<string> GetPreviewUrl(string cameraIndexCode, int streamType, string protocol, int transmode, string expand)
        {
            // 发起POST请求，超时时间15秒，返回响应字节数组
            var result = await _hikHttp.PostAsync<HikVisionResponse<PreviewUrlResponse>>("/api/video/v1/cameras/previewURLs", new
            {
                cameraIndexCode,
                streamType,
                protocol,
                transmode,
                expand
            });
            if (!result.Success)
            {
                _logger.LogInformation("/api/resource/v1/cameras/indexCode: POST fail");
                throw new MyException(result.Msg);
            }
            return result.Data?.Url;
        }
        /// <summary>
        /// <see cref="IVideoService.GetPlaybackUrlInfo(PlaybackUrlRequest)"/>
        /// </summary>
        public async Task<PlayackUrlResponse> GetPlaybackUrlInfo(PlaybackUrlRequest model)
        {
            // 发起POST请求，超时时间15秒，返回响应字节数组
            var result = await _hikHttp.PostAsync<HikVisionResponse<PlayackUrlResponse>>("/api/video/v1/cameras/playbackURLs", new
            {
                cameraIndexCode = model.CameraIndexCode,
                recordLocation = model.RecordLocation.ToString(),
                protocol = model.Protocol,
                transmode = model.Transmode,
                beginTime = model.BeginTime.GetTimeIosFormatter(),
                endTime = model.EndTime.GetTimeIosFormatter(),
                uuid = model.Uuid,
                expand = model.Expand
            });
            if (!result.Success)
            {
                throw new MyException(result.Msg);
            }
            return result.Data;
        }
        /// <summary>
        /// <see cref="IVideoService.GetPresets(string)"/>
        /// </summary>
        public async Task<ListBaseResponse<PresetsResponse>> GetPresets(string cameraIndexCode)
        {
            // 发起POST请求，超时时间15秒，返回响应字节数组
            var result = await _hikHttp.PostAsync<HikVisionResponse<ListBaseResponse<PresetsResponse>>>("/api/video/v1/presets/searches", new
            {
                cameraIndexCode
            });
            if (!result.Success)
            {
                throw new MyException(result.Msg);
            }
            return result.Data;
        }
        /// <summary>
        /// <see cref="IVideoService.Control(ControlModel)"/>
        /// </summary>
        public async Task<bool> Control(ControlModel control)
        {
            var result = await _hikHttp.PostAsync<HikVisionResponse>("/api/video/v1/ptzs/controlling", new
            {
                cameraIndexCode = control.CameraIndexCode,
                action = control.Action,
                command = control.Command.ToString(),
                speed = control.Speed,
                presetIndex = control.PresetIndex
            });
            if (!result.Success)
            {
                throw new MyException(result.Msg);
            }
            return result.Success;
        }
        /// <summary>
        /// <see cref="IVideoService.GetCameras(int,int)"/>
        /// </summary>
        public async Task<ListBaseResponse<CameraResponse>> GetCameras(int page, int size)
        {
            var result = await _hikHttp.PostAsync<HikVisionResponse<ListBaseResponse<CameraResponse>>>(
                "/api/resource/v1/cameras", new
                {
                    pageNo = page,
                    pageSize = size
                });
            return result?.Data;
        }
        /// <summary>
        /// <see cref="IVideoService.GetCameras(CameraRequest)"/>
        /// </summary>
        public async Task<ListBaseResponse<CameraResponse>> GetCameras(CameraRequest request)
        {
            var result = await _hikHttp.PostAsync<HikVisionResponse<ListBaseResponse<CameraResponse>>>(
                "/api/resource/v1/camera/advance/cameraList", new
                {
                    pageNo = request.PageNo,
                    pageSize = request.PageSize,
                    cameraIndexCodes = request.CameraIndexCodes != null ? string.Join(',', request.CameraIndexCodes) : "",
                    cameraName = request.CameraName,
                    encodeDevIndexCode = request.EncodeDevIndexCode,
                    regionIndexCode = request.RegionIndexCode,
                    isCascade = request.IsCascade
                });
            return result?.Data;
        }
    }
}