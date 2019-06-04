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
using Microsoft.Extensions.Configuration;

namespace General.Api.Application.Door
{
    /// <summary>
    /// 门禁点控制服务
    /// </summary>
    public class DoorControlService : IDoorControlService
    {
        private readonly string _doorControlApi;
        private readonly IConfiguration _configuration;
        /// <summary>
        /// construct
        /// </summary>
        public DoorControlService(IConfiguration configuration)
        {
            _configuration = configuration;
            _doorControlApi = configuration["hikvisionUrl:doorControl"];
            if (string.IsNullOrEmpty(_doorControlApi))
            {
                throw new MyException("doorControlApiUrl is null");
            }
        }
        /// <summary>
        /// <see cref="IDoorControlService.DoControl(List{string},int)"/>
        /// </summary>
        public async Task<List<Dto.DoorControlResponse>> DoControl(List<string> doorIndexCodeList, int controlType)
        {
            var data = await _doorControlApi.AppendFormat("/api/acs/v1/door/doControl")
                 .PostAsync(new { doorIndexCodes = doorIndexCodeList, controlType })
                 .ReciveJsonResultAsync<HikVisionResponse<List<Dto.DoorControlResponse>>>();
            return data?.Data;
        }
        /// <summary>
        /// <see cref="IDoorControlService.GetDoorList(int,int,List{string},string,string,string)"/>
        /// </summary>
        public async Task<ListBaseResponse<DoorInfoResponse>> GetDoorList(int pageNo, int pageSize, List<string> doorIndexCode, string doorName, string acsDevIndexCode,
            string regionIndexCode)
        {
            var data = await _doorControlApi.AppendFormat("/api/resource/v1/acsDoor/advance/acsDoorList")
                .PostAsync(new
                {
                    pageNo,
                    pageSize,
                    doorIndexCodes = (doorIndexCode?.Any() ?? false) ? string.Join(",", doorIndexCode).TrimEnd(',') : null,
                    doorName,
                    regionIndexCode,
                    acsDevIndexCode
                })
                .ReciveJsonResultAsync<HikVisionResponse<ListBaseResponse<DoorInfoResponse>>>();
            return data?.Data;
        }
        /// <summary>
        /// <see cref="IDoorControlService.GetRegionList(int,int,string)"/>
        /// </summary>
        public async Task<ListBaseResponse<RegionInfoResponse>> GetRegionList(int pageNo, int pageSize, string treeCode)
        {
            var data = await _doorControlApi.AppendFormat("/api/resource/v1/regions")
                .PostAsync(new
                {
                    pageNo,
                    pageSize,
                    treeCode
                })
                .ReciveJsonResultAsync<HikVisionResponse<ListBaseResponse<RegionInfoResponse>>>();
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

            var data = await _doorControlApi.AppendFormat("/api/acps/v1/authDownload/data/addition")
                .PostAsync(model)
                .ReciveJsonResultAsync<HikVisionResponse>();
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
            var data = await _doorControlApi.AppendFormat("/api/acps/v1/authDownload/task/addition")
                .PostAsync(new { taskType })
                .ReciveJsonResultAsync<HikVisionResponse<DoorAuthTaskResponse>>();
            return data?.Data?.TaskId;
        }
        /// <summary>
        /// <see cref="IDoorControlService.GetAuthProgress(string)"/>
        /// </summary>
        public async Task<Dto.DoorAuthTaskProgressResponse> GetAuthProgress(string taskId)
        {
            var data = await _doorControlApi.AppendFormat("/api/acps/v1/authDownload/task/progress")
                .PostAsync(new { taskId })
                .ReciveJsonResultAsync<HikVisionResponse<DoorAuthTaskProgressResponse>>();
            return data?.Data;
        }
    }
}