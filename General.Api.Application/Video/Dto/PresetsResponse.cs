namespace General.Api.Application.Video.Dto
{
    /// <summary>
    /// 预置点信息。
    /// </summary>
    public class PresetsResponse
    {
        /// <summary>
        /// 预置点名称
        /// </summary>
        public string PresetPointName { get; set; }
        /// <summary>
        /// 预置点编号
        /// </summary>
        public string  PresetPointIndex { get; set; }
        /// <summary>
        /// 监控点编号
        /// </summary>
        public string  CameraIndexCode { get; set; }
    }
}