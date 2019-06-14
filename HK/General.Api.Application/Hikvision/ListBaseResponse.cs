using System.Collections.Generic;

namespace General.Api.Application.Hikvision
{
    /// <summary>
    /// 列表
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ListBaseResponse<T> where T : class
    {
        /// <summary>
        /// 总数
        /// </summary>
        public int Total { get; set; }
        /// <summary>
        /// 页码
        /// </summary>
        public int PageNo { get; set; }
        /// <summary>
        /// 页容量
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 是否具有下一页
        /// </summary>
        public bool HasNextPage { get; set; }
        /// <summary>
        /// 是否有前一页
        /// </summary>
        public bool HasPreviousPage { get; set; }
        /// <summary>
        /// 数据数组
        /// </summary>
        public List<T> Rows { get; set; }
        /// <summary>
        /// 列表
        /// </summary>
        public List<T> List { get; set; }
    }
}
