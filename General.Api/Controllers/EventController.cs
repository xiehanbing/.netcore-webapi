using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using General.Api.Application.Event;
using General.Api.Application.Event.Dto;
using General.Api.Framework;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace General.Api.Controllers
{
    /// <summary>
    /// 事件服务
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;
        /// <summary>
        /// construct
        /// </summary>
        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }
        /// <summary>
        /// 获取订阅事件列表
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("list")]
        public async Task<ApiResult<Application.Event.Dto.SubscriptionInfoResponse>> GetSubscrList()
        {
            return new ApiResult<Application.Event.Dto.SubscriptionInfoResponse>(await _eventService.GetSubscrList());
        }
        /// <summary>
        /// 订阅事件
        /// </summary>
        /// <param name="eventTypes">事件</param>
        /// <param name="eventDest">地址</param>
        /// <returns></returns>
        [HttpGet, Route("subscr")]
        public async Task<ApiResult<bool>> Subscr([FromBody]List<int> eventTypes, string eventDest)
        {
            return new ApiResult<bool>(await _eventService.SubscrEvent(eventTypes, eventDest));
        }
        /// <summary>
        /// 取消订阅
        /// </summary>
        /// <param name="eventTypes">事件</param>
        /// <returns></returns>
        [HttpGet, Route("cancel")]
        public async Task<ApiResult<bool>> Cancel([FromBody]List<int> eventTypes)
        {
            return new ApiResult<bool>(await _eventService.Cancel(eventTypes));
        }
    }
}