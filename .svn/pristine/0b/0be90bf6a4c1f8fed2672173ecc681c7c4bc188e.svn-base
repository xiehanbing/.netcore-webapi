using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using General.Api.Application.Hikvision;
using General.Core;
using General.Core.HttpClient.Extension;

namespace General.Api.Application.Visitor
{
    /// <summary>
    /// 访客
    /// </summary>
    public class VisitorService : IVisitorService
    {
        private readonly string _doorControlApi;
        /// <summary>
        /// construct
        /// </summary>
        public VisitorService()
        {
            _doorControlApi = HikVisionContext.HikVisionBaseUrl;
            if (string.IsNullOrEmpty(_doorControlApi))
            {
                throw new MyException("doorControlApiUrl is null");
            }
        }
        /// <summary>
        /// <see cref="IVisitorService.AddVisitor(Request.VisitorAddRequest)"/>
        /// </summary>
        public async Task<Dto.VisitorAddResponse> AddVisitor(Request.VisitorAddRequest model)
        {
            var data = await _doorControlApi.AppendFormatToHik("/api/visitor/v1/appointment")
                .SetHiKSecreity()
                .PostAsync(model)
                .ReciveJsonResultAsync<HikVisionResponse<Dto.VisitorAddResponse>>();
            //todo 验证码处理
            return data?.Data;
        }
        /// <summary>
        /// <see cref="IVisitorService.UpdateVisitor(Request.VisitorAddRequest)"/>
        /// </summary>
        public async Task<Dto.VisitorAddResponse> UpdateVisitor(Request.VisitorAddRequest model)
        {
            var data = await _doorControlApi.AppendFormatToHik("/api/visitor/v1/appointment/update")
                .SetHiKSecreity()
                .PostAsync(model)
                .ReciveJsonResultAsync<HikVisionResponse<Dto.VisitorAddResponse>>();
            //todo 验证码处理
            return data?.Data;
        }
        /// <summary>
        /// <see cref="IVisitorService.GetVisitorList(Request.VisitorQueryRequest)"/>
        /// </summary>
        public async Task<ListBaseResponse<Dto.AppointmentRecordResponse>> GetVisitorList(
            Request.VisitorQueryRequest model)
        {
            var data = await _doorControlApi.AppendFormatToHik("/api/visitor/v1/appointment/records")
                .SetHiKSecreity()
                .PostAsync(model)
                .ReciveJsonResultAsync<HikVisionResponse<ListBaseResponse<Dto.AppointmentRecordResponse>>>();
            return data?.Data;
        }
        /// <summary>
        /// <see cref="IVisitorService.Cancel(List{string})"/>
        /// </summary>
        public async Task<bool> Cancel(List<string> appointRecordIds)
        {
            var data = await _doorControlApi.AppendFormatToHik("")
                .SetHiKSecreity()
                .PostAsync(new
                {
                    appointRecordIds
                })
                .ReciveJsonResultAsync<HikVisionResponse>();
            return data?.Success ?? false;
        }
        /// <summary>
        /// <see cref="IVisitorService.GetVisitingList(Request.VisitorQueryRequest)"/>
        /// </summary>
        public async Task<ListBaseResponse<Dto.AppointmentRecordResponse>> GetVisitingList(
            Request.VisitorQueryRequest model)
        {
            var data = await _doorControlApi.AppendFormatToHik("/api/visitor/v1/visiting/records")
                .SetHiKSecreity()
                .PostAsync(model)
                .ReciveJsonResultAsync<HikVisionResponse<ListBaseResponse<Dto.AppointmentRecordResponse>>>();
            return data?.Data;
        }
        /// <summary>
        /// <see cref="IVisitorService.GetVisitingPicture(string,string)"/>
        /// </summary>
        public async Task<string> GetVisitingPicture(string svrIndexCode, string picUri)
        {
            var data = await _doorControlApi.AppendFormatToHik("/api/visitor/v1/record/pictures")
                .SetHiKSecreity()
                .PostAsync(new
                {
                    svrIndexCode,
                    picUri
                })
                .ReciveResponseHeadersByKey("Location");
            return data?.FirstOrDefault();
        }
    }
}