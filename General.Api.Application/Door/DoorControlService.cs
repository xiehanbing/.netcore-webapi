using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using General.Api.Application.Door.Dto;
using General.Api.Application.Hikvision;
using General.Api.Core.Log;
using General.Core;
using General.Core.Extension;
using General.Core.HttpClient.Extension;
using General.EntityFrameworkCore.Log;
using HttpUtil;
using Microsoft.Extensions.Configuration;

namespace General.Api.Application.Door
{
    /// <summary>
    /// 门禁点控制服务
    /// </summary>
    public class DoorControlService : IDoorControlService
    {
        private readonly string _doorControlApi;
        private readonly IHikHttpUtillib _hikHttp;
        /// <summary>
        /// construct
        /// </summary>
        public DoorControlService(IHikHttpUtillib hikHttp)
        {
            _hikHttp = hikHttp;
            //_doorControlApi = configuration[HikVisionContext.HikVisionBaseApiName];
            //if (string.IsNullOrEmpty(_doorControlApi))
            //{
            //    throw new MyException("doorControlApiUrl is null");
            //}
        }
        /// <summary>
        /// <see cref="IDoorControlService.DoControl(List{string},int)"/>
        /// </summary>
        public async Task<List<Dto.DoorControlResponse>> DoControl(List<string> doorIndexCodeList, int controlType)
        {
            var data = await _hikHttp.PostAsync<HikVisionResponse<List<Dto.DoorControlResponse>>>("/api/acs/v1/door/doControl", new { doorIndexCodes = doorIndexCodeList, controlType });
            return data?.Data;
        }
        /// <summary>
        /// <see cref="IDoorControlService.GetDoorList(int,int,List{string},string,string,string)"/>
        /// </summary>
        public async Task<ListBaseResponse<DoorInfoResponse>> GetDoorList(int pageNo, int pageSize, List<string> doorIndexCode, string doorName, string acsDevIndexCode,
            string regionIndexCode)
        {
            var data = await _hikHttp.PostAsync<HikVisionResponse<ListBaseResponse<DoorInfoResponse>>>("/api/resource/v1/acsDoor/advance/acsDoorList", new
            {
                pageNo,
                pageSize,
                doorIndexCodes = (doorIndexCode?.Any() ?? false) ? string.Join(",", doorIndexCode).TrimEnd(',') : null,
                doorName,
                regionIndexCode,
                acsDevIndexCode
            });
            return data?.Data;
        }
        /// <summary>
        /// <see cref="IDoorControlService.GetRegionList(int,int,string)"/>
        /// </summary>
        public async Task<ListBaseResponse<RegionInfoResponse>> GetRegionList(int pageNo, int pageSize, string treeCode)
        {
            var data = await _hikHttp.PostAsync<HikVisionResponse<ListBaseResponse<RegionInfoResponse>>>("/api/resource/v1/regions", new
            {
                pageNo,
                pageSize,
                treeCode
            });
            return data?.Data;
        }

        /// <summary>
        /// <see cref="IDoorControlService.CreateDoorAuth(DoorAuthAddDto)"/>
        /// </summary>
        public async Task<string> CreateDoorAuth(Dto.DoorAuthAddDto model)
        {
            //todo create task
            var taskId = await CreateDoorAuthTask(model.TaskType.GetHashCode());
            if (taskId.IsNotWhiteSpace())
            {
                throw new MyException("创建task失败");
            }
            model.TaskId = taskId;

            var data = await _hikHttp.PostAsync<HikVisionResponse>("/api/acps/v1/authDownload/data/addition", model);
            if (data.Success)
            {
                return taskId;
            }
            throw new MyException("门禁点权限下发失败:" + data.Msg);

        }
        /// <summary>
        /// 创建门禁点权限任务
        /// </summary>
        /// <param name="taskType">任务类型</param>
        /// <returns></returns>
        private async Task<string> CreateDoorAuthTask(int taskType)
        {
            var data = await _hikHttp.PostAsync<HikVisionResponse<DoorAuthTaskResponse>>("/api/acps/v1/authDownload/task/addition", new { taskType });
            return data?.Data?.TaskId;
        }
        /// <summary>
        /// <see cref="IDoorControlService.GetAuthProgress(string)"/>
        /// </summary>
        public async Task<Dto.DoorAuthTaskProgressResponse> GetAuthProgress(string taskId)
        {
            var data = await _hikHttp.PostAsync<HikVisionResponse<DoorAuthTaskProgressResponse>>("/api/acps/v1/authDownload/task/progress", new { taskId });
            return data?.Data;
        }
        /// <summary>
        /// <see cref="IDoorControlService.GetEventList(Request.DoorEventQueryRequest)"/>
        /// </summary>
        public async Task<ListBaseResponse<Dto.DoorEventQueryResponse>> GetEventList(
            Request.DoorEventQueryRequest request)
        {
            var data = await _hikHttp.PostAsync<HikVisionResponse<ListBaseResponse<DoorEventQueryResponse>>>("/api/acs/v1/door/events", request);
            return data?.Data;
        }
        /// <summary>
        /// <see cref="IDoorControlService.GetEventPictures(string,string)"/>
        /// </summary>
        public async Task<string> GetEventPictures(string svrIndexCode, string picUri)
        {
            //Location
            var data = await _hikHttp.PostHttpWebResponseAsync("/api/acs/v1/event/pictures", new
            {
                svrIndexCode,
                picUri
            });
            return data?.Headers?.Get("Location");
        }
    }
}