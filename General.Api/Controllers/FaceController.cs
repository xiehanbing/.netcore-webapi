using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using General.Api.Application.Face;
using General.Api.Application.Hikvision;
using General.Api.Application.Video;
using General.Api.Application.Video.Dto;
using General.Api.Application.Video.Request;
using General.Api.Framework.Filters;
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
            var data = await _faceService.GetSearchList(request);
            if (data?.List?.Count > 0)
            {
                data.List = await MapCapture(data.List);
            }
            return data;
        }
        /// <summary>
        /// 获取检索数据
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private async Task<List<CaptureSearchResponse>> GetSearch(FaceSearchRequest request)
        {
            var data = await _faceService.GetSearchList(request);
            List<CaptureSearchResponse> list = new List<CaptureSearchResponse>();
            if (data?.List?.Count > 0)
            {
                list = await SearchMapCapture(data.List);
            }
            return list;
        }
        /// <summary>
        /// 以图搜图 搜索抓拍
        /// </summary>
        /// <param name="request">参数</param>
        /// <returns></returns>
        [HttpPost, Route("capture")]
        public async Task<ListBaseResponse<CaptureSearchResponse>> GetCapture([FromBody] CaptureSearchRequest request)
        {
            var response = new ListBaseResponse<CaptureSearchResponse>();
            if (string.IsNullOrWhiteSpace(request.FacePicUrl) && string.IsNullOrWhiteSpace(request.FacePicBinaryData))
            {
                response.List = await GetSearch(new FaceSearchRequest()
                {
                    AgeGroup = request.AgeGroup,
                    StartTime = request.StartTime,
                    EndTime = request.EndTime,
                    CameraIndexCodes = request.CameraIndexCodes,
                    Sex = request.Sex,
                    WithGlass = request.WithGlass,
                    PageSize = request.PageSize,
                    PageNo = request.PageNo
                });
                return response;
            }
            response = await _faceService.GetCaptureList(request);
            if (response?.List?.Count > 0)
            {
                response.List = await MapCapture(response.List);
            }
            return response;
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
        /// 映射属性
        /// </summary>
        /// <param name="captureList"></param>
        /// <returns></returns>
        private async Task<List<FaceSearchResponse>> MapCapture(List<FaceSearchResponse> captureList)
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
        /// 转换
        /// </summary>
        /// <param name="captureList"></param>
        /// <returns></returns>
        private async Task<List<CaptureSearchResponse>> SearchMapCapture(List<FaceSearchResponse> captureList)
        {
            string[] cameraIndexCodes = captureList.Select(o => o.CameraIndexCode).ToArray();
            var cameraInfos = await GetCameras(cameraIndexCodes);
            var cameraInfoList = cameraInfos?.List;
            List<CaptureSearchResponse> list = new List<CaptureSearchResponse>();
            foreach (var face in captureList)
            {
                var model = new CaptureSearchResponse()
                {
                    AgeGroup = face.AgeGroup,
                    Area = face.Area,
                    BkgPicUrl = face.BkgPicUrl,
                    CameraIndexCode = face.CameraIndexCode,
                    CameraName = face.CameraName,
                    CaptureTime = face.EventTime,
                    FacePicUrl = face.FacePicUrl,
                    Sex = face.Sex,
                    Similarity = face.Similarity,
                    WithGlass = face.WithGlass
                };
                model.CameraName = cameraInfoList?.FirstOrDefault(o => o.CameraIndexCode.Equals(model.CameraIndexCode))
                    ?.CameraName ?? "";
                model.Area = model.CameraName;
                list.Add(model);
            }


            return list;
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


            //如果是
            if (string.IsNullOrWhiteSpace(request.FacePicUrl) && string.IsNullOrWhiteSpace(request.FacePicBinaryData))
            {
                var response = new ListBaseResponse<CaptureSearchResponse> {List = new List<CaptureSearchResponse>()};

                var searchRequest = new FaceSearchRequest()
                {
                    AgeGroup = request.AgeGroup,
                    StartTime = request.StartTime,
                    EndTime = request.EndTime,
                    CameraIndexCodes = request.CameraIndexCodes,
                    Sex = request.Sex,
                    WithGlass = request.WithGlass,
                    PageSize = request.PageSize,
                    PageNo = request.PageNo
                };
                //判断是否还有下一页
                while (hasNext)
                {
                    var data = await GetSearch(searchRequest);
                    if (data?.Count > 0)
                    {
                        page++;
                        searchRequest.PageNo = page;
                        var mapList = await MapCapture(data);
                        response.List.AddRange(mapList);
                    }
                    else
                    {
                        hasNext = false;
                    }
                }
                return response;
            }

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
        [HttpPost, Route("capture/test"), SwaggerIgnore(true)]
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
        [HttpPost, Route("capture/test/all"), SwaggerIgnore(true)]
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