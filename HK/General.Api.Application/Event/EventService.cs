using System.Collections.Generic;
using System.Threading.Tasks;
using General.Api.Application.Hikvision;
using General.Core;
using General.Core.HttpClient.Extension;

namespace General.Api.Application.Event
{
    /// <summary>
    /// 事件服务
    /// </summary>
    public class EventService : IEventService
    {
        private readonly string _doorControlApi;
        /// <summary>
        /// construct
        /// </summary>
        public EventService()
        {
            _doorControlApi = HikVisionContext.HikVisionBaseUrl;
            if (string.IsNullOrEmpty(_doorControlApi))
            {
                throw new MyException("doorControlApiUrl is null");
            }
        }
        /// <summary>
        /// <see cref="IEventService.SubscrEvent(List{int},string)"/>
        /// </summary>
        public async Task<bool> SubscrEvent(List<int> eventTypes, string eventDest)
        {
            var data = await _doorControlApi.AppendFormatToHik("/api/eventService/v1/eventSubscriptionByEventTypes")
                .SetHiKSecreity()
                .PostAsync(new
                {
                    eventTypes,
                    eventDest
                })
                .ReciveJsonResultAsync<HikVisionResponse>();
            return data?.Success ?? false;
        }
        /// <summary>
        /// <see cref="IEventService.Cancel(List{int})"/>
        /// </summary>
        public async Task<bool> Cancel(List<int> eventTypes)
        {
            var data = await _doorControlApi.AppendFormatToHik("/api/eventService/v1/eventUnSubscriptionByEventTypes")
                .SetHiKSecreity()
                .PostAsync(new
                {
                    eventTypes
                })
                .ReciveJsonResultAsync<HikVisionResponse>();
            return data?.Success ?? false;
        }
        /// <summary>
        /// <see cref="IEventService.GetSubscrList()"/>
        /// </summary>
        public async Task<Dto.SubscriptionInfoResponse> GetSubscrList()
        {
            var data = await _doorControlApi.AppendFormatToHik("/api/eventService/v1/eventUnSubscriptionByEventTypes")
                .SetHiKSecreity()
                .PostAsync(new { })
                .ReciveJsonResultAsync<HikVisionResponse<Dto.SubscriptionInfoResponse>>();
            return data?.Data;
        }
    }
}