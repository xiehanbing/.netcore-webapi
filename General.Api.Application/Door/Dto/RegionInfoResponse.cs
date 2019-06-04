namespace General.Api.Application.Door.Dto
{
    /// <summary>
    /// 区域信息
    /// </summary>
    public class RegionInfoResponse
    {
        /// <summary>
        /// 区域唯一标识码
        /// </summary>
        public string IndexCode { get; set; }
        /// <summary>
        /// 区域名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 父区域唯一标识码
        /// </summary>
        public string ParentIndexCode { get; set; }
        /// <summary>
        /// 树编号
        /// </summary>
        public string TreeCode { get; set; }
    }
}