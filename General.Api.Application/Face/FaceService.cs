using System.Threading.Tasks;
using General.Api.Application.Hikvision;
using General.Core;
using General.Core.Extension;
using HttpUtil;

namespace General.Api.Application.Face
{
    /// <summary>
    /// 人脸应用
    /// </summary>
    public class FaceService : IFaceService
    {
        private readonly IHikHttpUtillib _hikHttp;
        private readonly IHikVisionClient _hikClient;
        /// <summary>
        /// 构造函数 
        /// </summary>
        /// <param name="hikHttp"></param>
        /// <param name="hikClient"></param>
        public FaceService(IHikHttpUtillib hikHttp, IHikVisionClient hikClient)
        {
            _hikHttp = hikHttp;
            _hikClient = hikClient;
        }

        /// <summary>
        /// <see cref="IFaceService.GetSearchList(FaceSearchRequest)"/>
        /// </summary>
        public async Task<ListBaseResponse<FaceSearchResponse>> GetSearchList(FaceSearchRequest request)
        {
            var response = await _hikHttp.PostAsync<HikVisionResponse<ListBaseResponse<FaceSearchResponse>>>(
                "/api/frs/v1/event/face_capture/search", new
                {
                    pageNo = request.PageNo,
                    pageSize = request.PageSize,
                    startTime = request.StartTime.GetTimeIosFormatter(),
                    endTim = request.EndTime.GetTimeIosFormatter(),
                    cameraIndexCodes = request.CameraIndexCodes,
                    age = request.Age,
                    gender = request.Gender,
                    glass = request.Glass
                });
            if (!response.Success)
            {
                throw new MyException(response.Msg);
            }
            return response.Data;
        }
        /// <summary>
        /// <see cref="IFaceService.GetCaptureList(CaptureSearchRequest)"/>
        /// </summary>
        public async Task<ListBaseResponse<CaptureSearchResponse>> GetCaptureList(CaptureSearchRequest request)
        {
            var response = await _hikClient.PostAsync<ListBaseResponse<CaptureSearchResponse>>(
                "/api/frs/v1/application/captureSearch", new
                {
                    pageNo = request.PageNo,
                    pageSize = request.PageSize,
                    startTime = request.StartTime.GetTimeIosFormatter(),
                    endTim = request.EndTime.GetTimeIosFormatter(),
                    cameraIndexCodes = request.CameraIndexCodes,
                    facePicBinaryData = request.FacePicBinaryData,
                    facePicUrl = request.FacePicUrl,
                    searchNum = request.SearchNum,
                    minSimilarity = request.MinSimilarity,
                    maxSimilarity = request.MaxSimilarity,
                    ageGroup = request.AgeGroup,
                    sex = request.Sex,
                    withGlass = request.WithGlass,
                    smile = request.Smile,
                    isEthnic = request.IsEthnic,
                    age = request.Age
                });
            return response.Data;
        }
    }
}