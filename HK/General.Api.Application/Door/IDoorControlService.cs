using System.Collections.Generic;
using System.Threading.Tasks;
using General.Api.Application.Hikvision;

namespace General.Api.Application.Door
{
    /// <summary>
    /// 门禁控制服务
    /// </summary>
    public interface IDoorControlService
    {
        /// <summary>
        /// 门禁反控操作
        /// </summary>
        /// <param name="doorIndexCodeList">门禁点唯一标识集合</param>
        /// <param name="controlType">反控操作类型0-常开 1-门闭 2-门开 3-常闭</param>
        /// <returns></returns>
        Task<List<Dto.DoorControlResponse>> DoControl(List<string> doorIndexCodeList, int controlType);
        /// <summary>
        /// 获取门禁点列表
        /// </summary>
        /// <param name="pageNo">页码</param>
        /// <param name="pageSize">页容量</param>
        /// <param name="doorIndexCode">门禁点唯一标识符集</param>
        /// <param name="doorName">门禁点名称</param>
        /// <param name="acsDevIndexCode">门禁设备唯一标识</param>
        /// <param name="regionIndexCode">所属区域唯一标识</param>
        /// <returns></returns>
        Task<ListBaseResponse<Dto.DoorInfoResponse>> GetDoorList(int pageNo, int pageSize, List<string> doorIndexCode,
            string doorName, string acsDevIndexCode, string regionIndexCode);
        /// <summary>
        /// 获取区域列表
        /// </summary>
        /// <param name="pageNo">页码</param>
        /// <param name="pageSize">页容量</param>
        /// <param name="treeCode">树编号 树编号（默认0，0代表国标树） 此字段为预留字段，暂时不用。 最大长度：32 </param>
        /// <returns></returns>
        Task<ListBaseResponse<Dto.RegionInfoResponse>> GetRegionList(int pageNo, int pageSize, string treeCode);
        /// <summary>
        /// 更新门禁点 门禁权限
        /// </summary>
        /// <param name="model">参数</param>
        /// <returns></returns>
        Task<string> CreateDoorAuth(Dto.DoorAuthAddDto model);
        /// <summary>
        /// 获取 下载任务进度
        /// </summary>
        /// <param name="taskId">任务id</param>
        /// <returns></returns>
        Task<Dto.DoorAuthTaskProgressResponse> GetAuthProgress(string taskId);
        /// <summary>
        /// 查询门禁事件
        /// </summary>
        /// <param name="request">请求</param>
        /// <returns></returns>
        Task<ListBaseResponse<Dto.DoorEventQueryResponse>> GetEventList(Request.DoorEventQueryRequest request);
        /// <summary>
        /// 获取门禁事件的图片
        /// </summary>
        /// <param name="svrIndexCode">图片存储服务器唯一标识</param>
        /// <param name="picUri">图片的相对地址</param>
        /// <returns></returns>
        Task<string> GetEventPictures(string svrIndexCode, string picUri);
    }
}