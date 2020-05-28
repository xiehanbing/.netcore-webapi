using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using General.Api.Application.Hikvision;
using General.Api.Application.Video;
using General.Api.Application.Video.Dto;
using General.Api.Application.Video.Request;
using General.Api.Framework.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace General.Api.Controllers
{
    /// <summary>
    /// 监控
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CameraController : ControllerBase
    {
        private readonly IVideoService _videoService;
        /// <summary>
        /// contract
        /// </summary>
        /// <param name="videoService"></param>
        public CameraController(IVideoService videoService)
        {
            _videoService = videoService;
        }
        /// <summary>
        /// 获取监控点预览取流URL
        /// </summary>
        /// <param name="request">请求参数</param>
        /// <returns></returns>
        [HttpPost, Route("preview")]
        public async Task<string> GetPreviewUrl(PreviewUrlRequest request)
        {
            return await _videoService.GetPreviewUrl(request.CameraIndexCode, request.StreamType, request.Protocol, request.Transmode, request.Expand);
        }

        /// <summary>
        /// 获取监控点回放取流URL
        /// </summary>
        /// <param name="model">参数</param>
        /// <returns></returns>
        [HttpPost, Route("playback")]
        public async Task<PlayackUrlResponse> GetPlaybackUrlInfo(PlaybackUrlRequest model)
        {
            return await _videoService.GetPlaybackUrlInfo(model);
        }

        /// <summary>
        /// 获取预置点列表
        /// </summary>
        /// <param name="cameraIndexCode">监控点唯一标识</param>
        /// <returns></returns>
        [HttpGet, Route("presets")]
        public async Task<ListBaseResponse<PresetsResponse>> GetPresets(string cameraIndexCode)
        {
            return await _videoService.GetPresets(cameraIndexCode);
        }

        /// <summary>
        /// 控制监控点
        /// </summary>
        /// <param name="control">控制参数</param>
        /// <returns></returns>
        [HttpPost, Route("control")]
        public async Task<bool> Control(ControlModel control)
        {
            return await _videoService.Control(control);
        }
        /// <summary>
        /// 获取监控点资源
        /// </summary>
        /// <param name="pageNo">页码</param>
        /// <param name="pageSize">页容量</param>
        /// <returns></returns>
        [HttpGet, Route("cameras")]
        public async Task<ListBaseResponse<CameraResponse>> GetCameras([Required]int pageNo, [Required] int pageSize)
        {
            return await _videoService.GetCameras(pageNo, pageSize);
        }
        /// <summary>
        /// 获取监控点资源
        /// </summary>
        /// <param name="request">参数</param>
        /// <returns></returns>
        [HttpPost, Route("cameras/condition")]
        public async Task<ListBaseResponse<CameraResponse>> GetCamerasByCondition(CameraRequest request)
        {
            return await _videoService.GetCameras(request);
        }
        /// <summary>
        /// 获取所有的监控点资源
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("cameras/all")]
        public async Task<ListBaseResponse<CameraResponse>> GetAllCameras()
        {
            int page = 1;
            int size = 500;
            bool hasNext = true;
            ListBaseResponse<CameraResponse> list = new ListBaseResponse<CameraResponse>();
            list.List = new List<CameraResponse>();
            //判断是否还有下一页
            while (hasNext)
            {
                var data = await _videoService.GetCameras(page, size);
                if (data.List?.Count > 0)
                {
                    page++;
                    list.List.AddRange(data.List);
                }
                else
                {
                    hasNext = false;
                }
            }

            return list;
        }
        /// <summary>
        /// 获取所有的监控点资源
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("cameras/all/test"), SwaggerIgnore(true)]
        public ListBaseResponse<CameraResponse> GetAllTestCameras()
        {
            ListBaseResponse<CameraResponse> list = new ListBaseResponse<CameraResponse>();
            list.List = new List<CameraResponse>();
            for (int i = 0; i < 50; i++)
            {
                list.List.Add(new CameraResponse()
                {
                    CameraIndexCode = i.ToString(),
                    CameraName = i + "相机"
                });
            }
            return list;
        }
    }
}