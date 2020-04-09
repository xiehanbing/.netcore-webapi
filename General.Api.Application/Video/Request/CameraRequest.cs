namespace General.Api.Application.Video.Request
{
    /// <summary>
    /// 查询监控点列表 条件
    /// </summary>
    public class CameraRequest:PageRequest
    {
        /// <summary>
        /// 监控点唯一标识集
        /// </summary>
        public string[] CameraIndexCodes { get; set; }
        /// <summary>
        /// 监控点名称（最大长度32）
        /// </summary>
        public string CameraName { get; set; }
        /// <summary>
        /// 所属编码设备唯一标识 （最大长度64）
        /// </summary>
        public string EncodeDevIndexCode { get; set; }
        /// <summary>
        /// 区域唯一标识；
        /// </summary>
        public string RegionIndexCode { get; set; }
        /// <summary>
        /// 0：非级联
        ///1：级联
        ///2：不限（包括级联和非级联）
        ///默认取值：2
        /// </summary>
        public int IsCascade { get; set; }
    }
}