using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using General.Api.Application.Hikvision;
using General.Api.Application.Visitor;
using General.Api.Application.Visitor.Dto;
using General.Api.Application.Visitor.Request;
using General.Api.Framework.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace General.Api.Controllers
{
    /// <summary>
    /// 访客记录
    /// </summary>
    [Route("api/[controller]"), Authorize]
    [ApiController]
    public class VisitorController : ControllerBase
    {
        private readonly IVisitorService _visitorService;
        /// <summary>
        /// construct
        /// </summary>
        public VisitorController(IVisitorService visitorService)
        {
            _visitorService = visitorService;
        }
        /// <summary>
        /// 访客预约
        /// </summary>
        /// <param name="model">参数</param>
        /// <returns></returns>
        [HttpPost,Route("")]
        public async Task<VisitorAddResponse> AddVisitor(VisitorAddRequest model)
        {
            return await _visitorService.AddVisitor(model);
        }
        /// <summary>
        /// 更新访客预约
        /// </summary>
        /// <param name="model">参数</param>
        /// <returns></returns>
        [HttpPut,Route("")]
        public async Task<VisitorAddResponse> UpdateVisitor(VisitorAddRequest model)
        {
            return await _visitorService.UpdateVisitor(model);
        }
        /// <summary>
        /// 查询访客记录
        /// </summary>
        /// <param name="model">参数</param>
        /// <returns></returns>
        [HttpPost,Route("list")]
        public async Task<ListBaseResponse<AppointmentRecordResponse>> GetVisitorList(VisitorQueryRequest model)
        {
            return await _visitorService.GetVisitorList(model);
        }
        /// <summary>
        /// 取消访客预约
        /// </summary>
        /// <param name="appointRecordIds">预约记录ID的数组</param>
        /// <returns></returns>
        [HttpPost,Route("cancel")]
        public async Task<bool> CancelVisitor(List<string> appointRecordIds)
        {
            return await _visitorService.Cancel(appointRecordIds);
        }
        /// <summary>
        /// 获取来访记录
        /// </summary>
        /// <param name="model">参数</param>
        /// <returns></returns>
        [HttpPost,Route("appointment/list")]
        public async Task<ListBaseResponse<AppointmentRecordResponse>> GetVisitingList(VisitorQueryRequest model)
        {
            return await _visitorService.GetVisitingList(model);
        }
        /// <summary>
        /// 获取来访记录图片
        /// </summary>
        /// <param name="svrIndexCode">图片存储服务器唯一标识</param>
        /// <param name="picUri">图片存储服务器唯一标识</param>
        /// <returns></returns>
        [HttpGet,Route("appointment/picture")]
        public async Task<string> GetVisitingPicture(string svrIndexCode, string picUri)
        {
            return await _visitorService.GetVisitingPicture(svrIndexCode, picUri);
        }
    }
}