using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using General.Api.Application.Event;
using General.Api.Application.Event.Dto;
using General.Api.Framework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace General.Api.Controllers
{
    /// <summary>
    /// 事件服务
    /// </summary>
    [Route("api/[controller]"), Authorize("General")]
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
        public async Task<Application.Event.Dto.SubscriptionInfoResponse> GetSubscrList()
        {
            return await _eventService.GetSubscrList();
        }
        /// <summary>
        /// 订阅事件
        /// </summary>
        /// <param name="eventTypes">事件</param>
        /// <param name="eventDest">地址</param>
        /// <returns></returns>
        [HttpGet, Route("subscr")]
        public async Task<bool> Subscr([FromBody]List<int> eventTypes, string eventDest)
        {
            return await _eventService.SubscrEvent(eventTypes, eventDest);
        }
        /// <summary>
        /// 取消订阅
        /// </summary>
        /// <param name="eventTypes">事件</param>
        /// <returns></returns>
        [HttpGet, Route("cancel")]
        public async Task<bool> Cancel([FromBody]List<int> eventTypes)
        {
            return await _eventService.Cancel(eventTypes);
        }
    }
}