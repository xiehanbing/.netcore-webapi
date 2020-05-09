using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using General.Api.Application.Face;
using General.Api.Application.Hikvision;
using General.Api.Application.Video;
using General.Api.Application.Video.Dto;
using General.Api.Application.Video.Request;
using General.Core.Extension;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace General.Api.Controllers
{
    /// <summary>
    /// 人脸应用
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class FaceController : ControllerBase
    {
        private readonly IFaceService _faceService;
        private readonly IVideoService _videoService;
        /// <summary>
        /// construct
        /// </summary>
        public FaceController(IFaceService faceService, IVideoService videoService)
        {
            _faceService = faceService;
            _videoService = videoService;
        }
        /// <summary>
        /// 按条件查询人脸抓拍事件
        /// </summary>
        /// <param name="request">参数</param>
        /// <returns></returns>
        [HttpPost, Route("search")]
        public async Task<ListBaseResponse<FaceSearchResponse>> Search([FromBody]FaceSearchRequest request)
        {
            return await _faceService.GetSearchList(request);
        }
        /// <summary>
        /// 以图搜图 搜索抓拍
        /// </summary>
        /// <param name="request">参数</param>
        /// <returns></returns>
        [HttpPost, Route("capture")]
        public async Task<ListBaseResponse<CaptureSearchResponse>> GetCapture([FromBody] CaptureSearchRequest request)
        {
            var data = await _faceService.GetCaptureList(request);
            if (data?.List?.Count > 0)
            {
                data.List = await MapCapture(data.List);
            }
            return data;
        }

        /// <summary>
        /// 获取相机信息
        /// </summary>
        /// <param name="cameraIndexCodes">相机code</param>
        /// <returns></returns>
        private async Task<ListBaseResponse<CameraResponse>> GetCameras(string[] cameraIndexCodes)
        {
            int cameraIndexCodesLength = cameraIndexCodes.Length;
            ListBaseResponse<CameraResponse> cameraInfos = new ListBaseResponse<CameraResponse>();
            cameraInfos.List = new List<CameraResponse>();
            if (cameraIndexCodesLength > 500)
            {
                bool hasCameraNext = true;
                int cameraPage = 1;
                int cameraSize = 500;
                while (hasCameraNext)
                {
                    var cameraInfo = await _videoService.GetCameras(new CameraRequest()
                    {
                        PageNo = cameraPage,
                        PageSize = cameraSize,
                        CameraIndexCodes = cameraIndexCodes
                    });
                    if (cameraInfo?.List?.Count > 0)
                    {
                        cameraInfos.List.AddRange(cameraInfo.List);
                        cameraPage++;
                    }
                    else
                    {
                        hasCameraNext = false;
                    }
                }
            }
            else
            {
                var cameraInfo = await _videoService.GetCameras(new CameraRequest()
                {
                    PageNo = 1,
                    PageSize = cameraIndexCodesLength,
                    CameraIndexCodes = cameraIndexCodes
                });
                if (cameraInfo?.List?.Count > 0)
                {
                    cameraInfos.List.AddRange(cameraInfo.List);
                }
            }

            return cameraInfos;
        }
        /// <summary>
        /// 映射属性
        /// </summary>
        /// <param name="captureList"></param>
        /// <returns></returns>
        private async Task<List<CaptureSearchResponse>> MapCapture(List<CaptureSearchResponse> captureList)
        {
            string[] cameraIndexCodes = captureList.Select(o => o.CameraIndexCode).ToArray();
            var cameraInfos = await GetCameras(cameraIndexCodes);
            if (cameraInfos.List?.Count > 0)
            {
                var cameraInfoList = cameraInfos.List;
                foreach (var item in captureList)
                {
                    item.CameraName =
                        cameraInfoList.FirstOrDefault(o => o.CameraIndexCode.Equals(item.CameraIndexCode))
                            ?.CameraName ?? "";
                    item.Area = item.CameraName;
                }
            }

            return captureList;
        }
        /// <summary>
        /// 以图搜图 搜索抓拍
        /// </summary>
        /// <param name="request">参数</param>
        /// <returns></returns>
        [HttpPost, Route("capture/all")]
        public async Task<ListBaseResponse<CaptureSearchResponse>> GetAllCapture([FromBody] CaptureSearchRequest request)
        {
            int page = 1;
            int size = 500;
            bool hasNext = true;
            request.PageNo = page;
            request.PageSize = size;
            ListBaseResponse<CaptureSearchResponse> list = new ListBaseResponse<CaptureSearchResponse>();
            //判断是否还有下一页
            while (hasNext)
            {
                var data = await _faceService.GetCaptureList(request);
                if (data.List?.Count > 0)
                {
                    page++;
                    request.PageNo = page;
                    var mapList = await MapCapture(data.List);
                    list.List.AddRange(mapList);
                }
                else
                {
                    hasNext = false;
                }
            }

            return list;

        }
        /// <summary>
        /// 以图搜图 搜索抓拍
        /// </summary>
        /// <param name="request">参数</param>
        /// <returns></returns>
        [HttpPost, Route("capture/test")]
        public ListBaseResponse<CaptureSearchResponse> GetTestCapture([FromBody] CaptureSearchRequest request)
        {
            ListBaseResponse<CaptureSearchResponse> list = new ListBaseResponse<CaptureSearchResponse>();
            list.List = new List<CaptureSearchResponse>();
            int[] socure = new[] { 80, 90, 100, 50 };
            int socureLength = socure.Length;
            int total = 200;
            int page = request.PageNo;
            int size = request.PageSize;
            if (((page - 1) * size + 1) >= total)
            {
                return list;
            }

            string[] image = new[] { "/wwwroot/image/demo.png", "/wwwroot/image/camera-other.png" };
            int imageLength = image.Length;
            Random random = new Random();
            for (int i = (page - 1) * size + 1; i <= page * size; i++)
            {
                list.List.Add(new CaptureSearchResponse()
                {
                    CameraIndexCode = i.ToString(),
                    Similarity = socure[(i % socureLength)].ToString("#.00"),
                    FacePicUrl = image[(i % imageLength)],
                    CameraName = i + "相机",
                    CaptureTime = DateTime.Now.GetTimeIosFormatter(),
                    Area = "一楼大门"
                });
            }

            list.Total = total;
            return list;
        }
        /// <summary>
        /// 以图搜图 搜索抓拍
        /// </summary>
        /// <param name="request">参数</param>
        /// <returns></returns>
        [HttpPost, Route("capture/test/all")]
        public ListBaseResponse<CaptureSearchResponse> GetAllTestCapture([FromBody] CaptureSearchRequest request)
        {
            ListBaseResponse<CaptureSearchResponse> list = new ListBaseResponse<CaptureSearchResponse>();
            list.List = new List<CaptureSearchResponse>();
            int[] socure = new[] { 80, 90, 100, 50 };
            int socureLength = socure.Length;
            int total = 200;
            int page = request.PageNo;
            int size = request.PageSize;
            if (((page - 1) * size + 1) >= total)
            {
                return list;
            }
            string[] image = new[] { "/wwwroot/image/demo.png", "/wwwroot/image/camera-other.png" };
            int imageLength = image.Length;
            Random random = new Random();
            for (int i = 0; i <= total; i++)
            {
                list.List.Add(new CaptureSearchResponse()
                {
                    CameraIndexCode = i.ToString(),
                    Similarity = socure[(i % socureLength)].ToString("#.00"),
                    FacePicUrl = image[(i % imageLength)],
                    CameraName = i + "相机",
                    CaptureTime = DateTime.Now.GetTimeIosFormatter(),
                    Area = "一楼大门"
                });
            }

            list.Total = total;
            return list;
        }
    }
}