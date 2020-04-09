using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using General.Api.Application.Face;
using General.Api.Application.Hikvision;
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
        /// <summary>
        /// construct
        /// </summary>
        /// <param name="faceService"></param>
        public FaceController(IFaceService faceService)
        {
            _faceService = faceService;
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
            return await _faceService.GetCaptureList(request);
        }
    }
}