using System.Threading.Tasks;
using General.Api.Application.Hikvision;
using General.Api.Application.Video.Dto;
using General.Api.Application.Video.Request;
using General.Core.HttpClient.Extension;

namespace General.Api.Application.Video
{
    /// <summary>
    /// 视频服务
    /// </summary>
    public class VideoService : IVideoService
    {
        private readonly string _videoUrl = HikVisionContext.HikVisionBaseUrl;
        /// <summary>
        /// <see cref="IVideoService.GetPreviewUrl(string,int,string,int,string)"/>
        /// </summary>
        public async Task<string> GetPreviewUrl(string cameraIndexCode, int streamType, string protocol, int transmode, string expand)
        {
            var data = await _videoUrl.AppendFormatToHik("/api/video/v1/cameras/previewURLs")
                .SetHiKSecreity()
                .PostAsync(new
                {
                    cameraIndexCode,
                    streamType,
                    protocol,
                    transmode,
                    expand
                })
                .ReciveJsonResultAsync<HikVisionResponse<PreviewUrlResponse>>();
            return data?.Data?.Url;
        }
        /// <summary>
        /// <see cref="IVideoService.GetPlaybackUrlInfo(PlaybackUrlRequest)"/>
        /// </summary>
        public async Task<ListBaseResponse<PlayackUrlResponse>> GetPlaybackUrlInfo(PlaybackUrlRequest model)
        {
            var data = await _videoUrl.AppendFormatToHik("/api/video/v1/cameras/playbackURLs")
                .SetHiKSecreity()
                .PostAsync(model)
                .ReciveJsonResultAsync<HikVisionResponse<ListBaseResponse<PlayackUrlResponse>>>();
            return data?.Data;
        }
    }
}