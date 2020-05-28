using System;
using System.Threading.Tasks;
using General.Api.Application.Hikvision;
using General.Api.Application.Parking.Dto.ChargeBill;

namespace General.Api.Application.Parking
{
    /// <summary>
    /// 停车场记录
    /// </summary>
    public interface IParkRecordService
    {
        /// <summary>
        /// 查询场内车辆停车信息
        /// </summary>
        /// <param name="parkSysCode">停车库唯一标识码</param>
        /// <param name="plateNo">车牌号码</param>
        /// <param name="pageNo">页码</param>
        /// <param name="pageSize">页容量</param>
        /// <returns></returns>
        Task<ListBaseResponse<Dto.Record.TempCarInRecordResponse>> GetTempRecordList(string parkSysCode, string plateNo,
            int pageNo, int pageSize);
        /// <summary>
        /// 查询车辆在车位上的停车信息
        /// </summary>
        /// <param name="model">请求参数</param>
        /// <returns></returns>
        Task<ListBaseResponse<Dto.Record.ParkRecoreResponse>>
            GetRecordList(Request.Record.QueryParkRecordRequest model);
        /// <summary>
        /// 查询过车记录
        /// </summary>
        /// <param name="parkSysCode">停车库唯一标识</param>
        /// <param name="entranceSysCode">出入口唯一标识</param>
        /// <param name="plateNo">车牌号</param>
        /// <param name="startTime">查询开始时间</param>
        /// <param name="endTime">查询结束时间</param>
        /// <param name="pageNo">目标页码</param>
        /// <param name="pageSize">每页记录数</param>
        /// <returns></returns>
        Task<ListBaseResponse<Dto.Record.CrossRecordResponse>> GetCrossRecord(string parkSysCode,
            string entranceSysCode, string plateNo, DateTime? startTime, DateTime? endTime, int pageNo, int pageSize);
        /// <summary>
        /// 查询车辆抓拍图片
        /// </summary>
        /// <param name="aswSysCode">图片服务唯一标识码</param>
        /// <param name="picUri">图片uri 根据【查询过车记录】、【查询临时车停车信息】或【查询停车账单】接口获取返回参数vehiclePicUri或plateNoPicUri，最大长度：256</param>
        /// <returns></returns>
        Task<string> GetCorssVehicleImage(string aswSysCode, string picUri);
        /// <summary>
        /// 查询过车记录 带有缴费金额
        /// </summary>
        /// <param name="parkSysCode">停车库唯一标识</param>
        /// <param name="entranceSysCode">出入口唯一标识</param>
        /// <param name="plateNo">车牌号</param>
        /// <param name="startTime">查询开始时间</param>
        /// <param name="endTime">查询结束时间</param>
        /// <param name="pageNo">目标页码</param>
        /// <param name="pageSize">每页记录数</param>
        /// <returns></returns>
        Task<ListBaseResponse<Dto.Record.CrossRecordResponse>> GetCrossRecordV2(string parkSysCode,
            string entranceSysCode, string plateNo, DateTime? startTime, DateTime? endTime, int pageNo, int pageSize);
        /// <summary>
        /// 查询缴费列表
        /// </summary>
        /// <param name="parkSysCode">停车库唯一标识</param>
        /// <param name="plateNo">车牌号</param>
        /// <param name="starTime">查询开始时间，ISO8601格式：
        ///yyyy-MM-ddTHH:mm:ss+当前时区，例如北京时间：
        ///2018-07-26T15:00:00+08:00</param>
        /// <param name="endTime">查询结束时间，ISO8601格式：
        ///yyyy-MM-ddTHH:mm:ss+当前时区，例如北京时间：
        ///2018-07-26T15:00:00+08:00</param>
        /// <param name="pageNo">目标页码</param>
        /// <param name="pageSize">每页记录数</param>
        /// <returns></returns>
        Task<ListBaseResponse<ChargeBillResponse>> GetBillList(string parkSysCode, string plateNo, DateTime? starTime, DateTime? endTime, int pageNo, int pageSize);
    }
}