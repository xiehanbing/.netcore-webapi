using System.Collections.Generic;
using System.Threading.Tasks;
using General.Api.Application.Hikvision;

namespace General.Api.Application.Face
{
    /// <summary>
    /// 人脸应用
    /// </summary>
    public interface IFaceService
    {
        /// <summary>
        /// 获取人脸抓拍查询列表
        /// </summary>
        /// <param name="request">请求参数</param>
        /// <returns></returns>
        Task<ListBaseResponse<FaceSearchResponse>> GetSearchList(FaceSearchRequest request);
        /// <summary>
        /// 以图搜图
        /// </summary>
        /// <param name="request">请求参数</param>
        /// <returns></returns>
        Task<ListBaseResponse<CaptureSearchResponse>> GetCaptureList(CaptureSearchRequest request);
    }
}