using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using General.Api.Application.Door;
using General.Api.Application.Door.Dto;
using General.Api.Application.Hikvision;
using General.Api.Framework;
using General.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace General.Api.Controllers
{
    /// <summary>
    /// 门禁点api
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class DoorControlController : ControllerBase
    {
        private readonly IDoorControlService _doorControlService;
        /// <summary>
        /// construct
        /// </summary>
        public DoorControlController(IDoorControlService doorControlService)
        {
            _doorControlService = doorControlService;
        }
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
        [Route("door/list")]
        [HttpGet]
        public async Task<ListBaseResponse<DoorInfoResponse>> GetDoorList(int pageNo, int pageSize, [FromQuery]List<string> doorIndexCode,
            string doorName, string acsDevIndexCode, string regionIndexCode)
        {
            return  await _doorControlService.GetDoorList(pageNo, pageSize, doorIndexCode, doorName, acsDevIndexCode, regionIndexCode);
        }
        /// <summary>
        /// 获取门禁点权限的更新进度
        /// </summary>
        /// <param name="taskId">任务id</param>
        /// <returns></returns>
        [Route("door/auth/progress"), HttpGet]
        public async Task<DoorAuthTaskProgressResponse> GetTaskProgress(string taskId)
        {
            return await _doorControlService.GetAuthProgress(taskId);
        }
        /// <summary>
        /// 创建更新 门禁点权限
        /// </summary>
        /// <param name="model">参数</param>
        /// <returns></returns>
        [Route("door/auth"), HttpPost]
        public async Task<string> UpdateDoorAuth(DoorAuthAddDto model)
        {
            return await _doorControlService.CreateDoorAuth(model);
        }
        /// <summary>
        /// 门禁点反控
        /// </summary>
        /// <param name="doorIndexCodeList">门禁点唯一标识集合</param>
        /// <param name="controlType">反控操作类型0-常开 1-门闭 2-门开 3-常闭</param>
        /// <returns></returns>
        [Route("door/doControl"), HttpGet]
        public async Task<List<DoorControlResponse>> DoControl([Required,FromBody]List<string> doorIndexCodeList,
            [Required]int controlType)
        {
            if (doorIndexCodeList.Count <= 0) throw new ValidatorException("doorIndexCodeList can't  null");
            return await _doorControlService.DoControl(doorIndexCodeList, controlType);
        }
        /// <summary>
        /// 获取区域列表
        /// </summary>
        /// <param name="pageNo">页码</param>
        /// <param name="pageSize">页容量</param>
        /// <param name="treeCode">树编号 树编号（默认0，0代表国标树） 此字段为预留字段，暂时不用。 最大长度：32 </param>
        /// <returns></returns>
        [Route("door/region/list"),HttpGet]
        public async Task<ListBaseResponse<RegionInfoResponse>> GetRegionList(int pageNo, int pageSize,
            string treeCode)
        {
            return await _doorControlService.GetRegionList(pageNo, pageSize, treeCode);
        }
    }
}