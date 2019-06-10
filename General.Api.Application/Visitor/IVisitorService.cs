using System.Collections.Generic;
using System.Threading.Tasks;
using General.Api.Application.Hikvision;

namespace General.Api.Application.Visitor
{
    /// <summary>
    /// 访客
    /// </summary>
    public interface IVisitorService
    {
        /// <summary>
        /// 访客预约
        /// </summary>
        /// <param name="model">参数</param>
        /// <returns></returns>
        Task<Dto.VisitorAddResponse> AddVisitor(Request.VisitorAddRequest model);
        /// <summary>
        /// 修改访客记录
        /// </summary>
        /// <param name="model">参数</param>
        /// <returns></returns>
        Task<Dto.VisitorAddResponse> UpdateVisitor(Request.VisitorAddRequest model);
        /// <summary>
        /// 查询访客记录
        /// </summary>
        /// <param name="model">参数</param>
        /// <returns></returns>
        Task<ListBaseResponse<Dto.AppointmentRecordResponse>> GetVisitorList(Request.VisitorQueryRequest model);
        /// <summary>
        /// 取消访客预约
        /// </summary>
        /// <param name="appointRecordIds">预约记录ID的数组</param>
        /// <returns></returns>
        Task<bool> Cancel(List<string> appointRecordIds);

        /// <summary>
        /// 获取来访记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<ListBaseResponse<Dto.AppointmentRecordResponse>> GetVisitingList(Request.VisitorQueryRequest model);
        /// <summary>
        /// 获取来访记录图片
        /// </summary>
        /// <param name="svrIndexCode">图片存储服务器唯一标识</param>
        /// <param name="picUri">图片存储服务器唯一标识</param>
        /// <returns></returns>
        Task<string> GetVisitingPicture(string svrIndexCode, string picUri);
    }
}