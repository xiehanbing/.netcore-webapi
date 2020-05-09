namespace General.Api.Application.Face
{
    /// <summary>
    /// 以图搜图结果
    /// </summary>
    public class CaptureSearchResponse
    {
        /// <summary>
        /// 抓拍到人脸的通道的唯一标识
        /// </summary>
        public string CameraIndexCode { get; set; }
        /// <summary>
        /// 监控点名称
        /// </summary>
        public string CameraName { get; set; }
        /// <summary>
        /// 地点区域
        /// </summary>
        public string  Area { get; set; }
        /// <summary>
        /// 抓拍到人脸的绝对时标，遵守ISO8601标准
        /// </summary>
        public string CaptureTime { get; set; }
        /// <summary>
        /// 抓拍到的人脸的性别，1-男性，2-女性，UNKNOWN-未知
        /// </summary>
        public string Sex { get; set; }
        /// <summary>
        /// 抓拍到的人脸年龄段
        /// </summary>
        public string AgeGroup { get; set; }
        /// <summary>
        /// 抓拍到的人脸是否戴眼镜
        /// </summary>
        public string WithGlass { get; set; }
        /// <summary>
        /// 抓拍到的人脸和上传人脸的相似度
        /// </summary>
        public string Similarity { get; set; }
        /// <summary>
        /// 抓拍到的人脸的背景图片
        /// </summary>
        public string BkgPicUrl { get; set; }
        /// <summary>
        /// 抓拍到的人脸的人脸图片，注：若是超脑抓拍图，facePicUrl与bkgPicUrl相同，都是背景图片，需要使用rect坐标字段进行人脸抠图
        /// </summary>
        public string FacePicUrl { get; set; }
        /// <summary>
        /// 超脑抓拍图的坐标，rect里面四个数字，依次是height，width，x，y。可用于人脸抠图。
        /// </summary>
        public string Rect { get; set; }
    }
}