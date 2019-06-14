using System.Collections.Generic;
using System.Threading.Tasks;

namespace General.Api.Application.Event
{
    /// <summary>
    /// 事件服务
    /// </summary>
    public interface IEventService
    {
        /// <summary>
        /// 按事件类型订阅事件
        /// </summary>
        /// <param name="eventTypes">事件类型</param>
        /// <param name="eventDest">指定事件接收的地址，采用restful回调模式，支持http和https，样式如下：http://ip:port/eventRcv或者 https://ip:port/eventRcv</param>
        /// <returns></returns>
        Task<bool> SubscrEvent(List<int> eventTypes, string eventDest);
        /// <summary>
        /// 取消订阅事件
        /// </summary>
        /// <param name="eventTypes">事件类型</param>
        /// <returns></returns>
        Task<bool> Cancel(List<int> eventTypes);

        /// <summary>
        /// 获取所有的订阅
        /// </summary>
        /// <returns></returns>
        Task<Dto.SubscriptionInfoResponse> GetSubscrList();
    }
}